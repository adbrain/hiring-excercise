<# 
.SYNOPSIS 
    Creates a Windows Azure VM with a MongoDB Docker container deployed to it.   
.DESCRIPTION  
    Creates a new Windows Azure Virtual Machine running Docker and deploys a 
    container running MongoDB.
    
    The VM provisioned is A1 (size Small) running Ubuntu Server 14.04 LTS.
    
    An endpoint for MongoDB is exposed on tcp port 27017.
    
    Assumes the Affinity Group already exists. 
    The storage account will be created if it doesn't exist.
.EXAMPLE 
    ./New-AzureMongoDbVM.ps1 -SubscriptionName "Free Trial" `
                             -StorageAccountName "adbrainchallenge" `
                             -AffinityGroupName "adbrainchallenge" `
                             -VMName "Panos-Adbrain-Dev-Ops-Challenge" `
                             -VMUser "panos" `
                             -VMPassword "Mw3t7?]KB@"
#> 

Param(  
    [Parameter(Mandatory = $true)]
    [string]$SubscriptionName,
    
    [Parameter(Mandatory = $true)]
    [string]$StorageAccountName,
    
    [Parameter(Mandatory = $true)]  
    [string]$AffinityGroupName, 

    [Parameter(Mandatory = $true)] 
    [string]$VMName,
    
    [Parameter(Mandatory = $true)] 
    [string]$VMUser,
    
    [Parameter(Mandatory = $true)] 
    [string]$VMPassword,
    
    [string]$VMServiceName)
    
# Check if Windows Azure PowerShell is available.
if ((Get-Module -ListAvailable Azure) -eq $null) 
{ 
    throw "Windows Azure PowerShell not found! Please install Windows PowerShell command-line tools from http://www.windowsazure.com/en-us/downloads/"
} 

Set-AzureSubscription -SubscriptionName $SubscriptionName

# Check Affinity Group exists.
$AffinityGroup = Get-AzureAffinityGroup | Where-Object { $_.Name -eq $AffinityGroupName }
if($AffinityGroup -eq $null)
{
    throw "Affinity group '$AffinityGroupName' does not exist."
}

$Location = $AffinityGroup.Location
Write-Host "Location is '$Location'"

# Create storage account if it does not already exist. 
$storage = Get-AzureStorageAccount | Where-Object { $_.StorageAccountName -eq $StorageAccountName } 
if($storage -eq $null)  
{ 
    Write-Host "Storage account '$StorageAccountName' does not exist. Creating storage account." 
    New-AzureStorageAccount -StorageAccountName $StorageAccountName -AffinityGroup $AffinityGroupName
} 

# Set current storage account.
Set-AzureSubscription -SubscriptionName $SubscriptionName -CurrentStorageAccount $StorageAccountName

# Create a container to upload the setup script.
$SetupScript = "setup-docker-mongodb.sh"
$guid = [guid]::NewGuid()
$TmpContainerName = "tmp-$guid"
$BlobName = $SetupScript
New-AzureStorageContainer -Name $TmpContainerName -Permission Blob
Write-Host "Creating storage container '$TmpContainerName' and uploading script $SetupScript in blob '$BlobName'."
# Upload setup script.
Set-AzureStorageBlobContent -Container $TmpContainerName `
                            -File .\$SetupScript `
                            -Blob $BlobName

$VMImageLabel = "Ubuntu Server 14.04 LTS"
# Find the latest image in the specific location
$VMImageName = @(Get-AzureVMImage `
                 | Where-Object -Property Label -Match $VMImageLabel `
                 | Where-Object -Property Location -Match $Location `
                 | Sort-Object PublishedDate -descending).ImageName[0]
Write-Host "Image to be used: '$VMImageName'"

$VMInstanceSize = "Small" # Small is A0
Write-Host "Instance size to be used: '$VMInstanceSize'"

# Set service name if one was not provided.
if ($VMServiceName -ne $null) { $VMServiceName = "${VMName}-Service" }
Write-Host "Service name to be used is '$VMServiceName'."

Write-Host "Creating virtual machine: '$VMName'"
New-AzureQuickVM –Linux `
                 -ImageName $VMImageName `
                 -Name $VMName `
                 -ServiceName $VMServiceName `
                 -AffinityGroup $AffinityGroupName `
                 -InstanceSize $VMInstanceSize `
                 –LinuxUser $VMUser `
                 -Password $VMPassword

$vm = Get-AzureVM -Name $VMName -ServiceName $VMServiceName
                       
# Configure the virtual machine if it was successfully created.
if ($vm -ne $null) {             
    Write-Host "Running script $SetupScript on new VM."
    #
    # To see the log of running the script, ssh to the server and see log here:
    # /var/log/azure/Microsoft.OSTCExtensions.CustomScriptForLinux/1.1/extension.log
    #
    
    $blobUri = "https://$StorageAccountName.blob.core.windows.net/$TmpContainerName/$BlobName"
    Write-Host "Blob Uri is $blobUri"
    
    $PublicConfiguration = '{"fileUris":["' + $blobUri + '"], "commandToExecute": "bash ' + $SetupScript + '" }'
        
    Write-Host "Public configuration to be used:"
    Write-Host "$PublicConfiguration"
    $CustomExtensionName = "CustomScriptForLinux"
    $CustomExtensionPublisher = "Microsoft.OSTCExtensions"
    $CustomExtensionVersion = "1.1"
    # Run setup script
    $vm | Set-AzureVMExtension -ExtensionName $CustomExtensionName `
                               -Publisher $CustomExtensionPublisher `
                               -Version $CustomExtensionVersion `
                               -PublicConfiguration $PublicConfiguration `
        | Update-AzureVM
		
    $MongoDBPort = 27017
    Write-Host "Creating endpoint for MongoDB on tcp port $MongoDBPort"
    # Expose endpoint for MongoDB on tcp port 27017
    $vm | Add-AzureEndpoint -Name "MongoDB"  `
                            -Protocol "tcp" `
                            -PublicPort $MongoDBPort `
                            -LocalPort $MongoDBPort `
        | Update-AzureVM
}

# Delete temporary storage container
# Todo: Find a way to wait until the custom script has finished and then remove the temporary container.
#Write-Host "Deleting temporary storage container '$TmpContainerName'"
#Remove-AzureStorageContainer -Name $TmpContainerName -Force

Write-Host "Once the docker container is set up, you can test that the mongodb is live by accessing on your browser:"
Write-Host "$VMServiceName.cloudapp.net:$MongoDBPort" 