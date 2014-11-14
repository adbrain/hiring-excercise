# Task
Implement a Powershell script that will provision a Virtual Machine on Azure running any Linux distribution of your choice and deploy a standard Docker container containing MongoDB to that machine.

# Specification
+ The Virtual Machine that is provisioned must be an A1 virtual machine.
+ The Virtual Machine that is provisioned must expose an endpoint for communication with MongoDB (The standard port is 27017).
+ The script must somehow be able to install docker on the Virtual Machine before or during the deployment process. You may use Cloud Storage, Linux scripting or any third-party Powershell tool in order to complete this step.
+ Docker must be running with the mongodb instance.
The docker image to use can be found here: https://registry.hub.docker.com/u/dockerfile/mongodb/

# Context 
The aim of this exercise is to test knowledge of the Microsoft Azure REST API, Powershell and general system administration. There is no need to provide any access keys in your solution.

# Goal
Focus on building a solution which is robust and portable so that the Powershell script can be executed on any machine against any environment. If you solution is able to work across machines, no further work is needed. Be creative!