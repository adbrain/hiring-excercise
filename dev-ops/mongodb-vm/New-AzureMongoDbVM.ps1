$SubscriptionName = "Free Trial"
$StorageAccount = "codiply"

Set-AzureSubscription -SubscriptionName $SubscriptionName  -CurrentStorageAccount $StorageAccount

$VMImageLabel = "Ubuntu Server 14.04 LTS"

# Find the latest image
$VMImageName = @(Get-AzureVMImage `
                 | Where-Object -Property Label -Match $VMImageLabel `
                 | Sort-Object PublishedDate -descending).ImageName[0]

Write-Host "Image to be used: $VMImageName"

$VMAffinityGroup = "Codiply"
$VMName = "Panos-Adbrain-Dev-Ops-Challenge"
$VMServiceName = "${VMName}-Service"
$VMUser = "codiply"
$VMPassword = "Mw3t7?]KB@"
$VMInstanceSize = "Small" # Small is A0

Write-Host "Creating VM $VMName"

New-AzureQuickVM –Linux `
                 -ImageName $VMImageName `
                 -Name $VMName `
                 -ServiceName $VMServiceName `                 `
                 -AffinityGroup $VMAffinityGroup `
                 -InstanceSize $VMInstanceSize `
                 –LinuxUser $VMUser `
                 -Password $VMPassword
                 
Write-Host "Creating endpoint for MongoDB"
                 
Get-AzureVM -Name $VMName `
            -ServiceName $VMServiceName `
| Add-AzureEndpoint -Name "MongoDB"  `
                    -Protocol "tcp" `
                    -PublicPort 27017 `
                    -LocalPort 27017 `
| Update-AzureVM