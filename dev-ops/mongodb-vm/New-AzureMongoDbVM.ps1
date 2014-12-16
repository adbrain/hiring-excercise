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

$VerbosePreference = "Continue"
    
# Check if Windows Azure PowerShell is available.
if ((Get-Module -ListAvailable Azure) -eq $null) 
{ 
    throw "Windows Azure PowerShell not found! Please install Windows PowerShell command-line tools from http://www.windowsazure.com/en-us/downloads/"
} 

# Set service name if one was not provided.
if ($VMServiceName -ne $null) { $VMServiceName = "${VMName}-Service" }
Write-Verbose "Service name to be used is '$VMServiceName'."

Set-AzureSubscription -SubscriptionName $SubscriptionName

# Check Affinity Group exists.
$AffinityGroup = Get-AzureAffinityGroup | Where-Object { $_.Name -eq $AffinityGroupName }
if($AffinityGroup -eq $null)
{
    throw "Affinity group '$AffinityGroupName' does not exist."
}

$Location = $AffinityGroup.Location

Write-Verbose "Location is '$Location'"

# Create storage account if it does not already exist. 
$storage = Get-AzureStorageAccount | Where-Object { $_.StorageAccountName -eq $StorageAccountName } 
if($storage -eq $null)  
{ 
    Write-Verbose "Storage account '$StorageAccountName' does not exist. Creating storage account." 
    New-AzureStorageAccount -StorageAccountName $StorageAccountName -AffinityGroup $AffinityGroupName
} 

# Set current storage account.
Set-AzureSubscription -SubscriptionName $SubscriptionName -CurrentStorageAccount $StorageAccountName

# Create a container to upload the scripts.
$guid = [guid]::NewGuid()
$TmpStorageContainerName = "tmp-$guid"
New-AzureStorageContainer -Name $TmpStorageContainerName

Set-AzureStorageBlobContent -Container $TmpStorageContainerName `
                            -File .\setup-docker-mongodb.sh `
                            -Blob setup

$VMImageLabel = "Ubuntu Server 14.04 LTS"
# Find the latest image in the specific location
$VMImageName = @(Get-AzureVMImage `
                 | Where-Object -Property Label -Match $VMImageLabel `
                 | Where-Object -Property Location -Match $Location `
                 | Sort-Object PublishedDate -descending).ImageName[0]
Write-Verbose "Image to be used: '$VMImageName'"

$VMInstanceSize = "Small" # Small is A0
Write-Verbose "Instance size to be used: '$VMInstanceSize'"

Write-Verbose "Creating virtual machine: '$VMName'"
$vm = New-AzureQuickVM –Linux `
                       -ImageName $VMImageName `
                       -Name $VMName `
                       -ServiceName $VMServiceName `
                       -AffinityGroup $AffinityGroupName `
                       -InstanceSize $VMInstanceSize `
                       –LinuxUser $VMUser `
                       -Password $VMPassword

# Configure the virtual machine if it was successfully created.
if ($vm -ne $null) {             
    Write-Verbose "Creating endpoint for MongoDB"

    Get-AzureVM -Name $VMName `
                -ServiceName $VMServiceName `
    | Add-AzureEndpoint -Name "MongoDB"  `
                        -Protocol "tcp" `
                        -PublicPort 27017 `
                        -LocalPort 27017 `
    | Update-AzureVM
}