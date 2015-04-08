# Task
Implement a BASH shell script that will provision an EC2 instance on AWS running any Linux distribution of your choice and deploy a standard Docker container containing MongoDB to that machine.

# Specification
+ The Virtual Machine that is provisioned must be a t2.micro.
+ The Virtual Machine that is provisioned must expose an endpoint for communication with MongoDB (The standard port is 27017)
+ The script must somehow be able to install docker on the Virtual Machine before or during the deployment process. You may use any appropriate method (e.g. EC2 container service, Chef Puppet script, custom AMI, user data etc) to install and run docker but  docker must be running with the mongodb instance.
+ The docker image to use can be found here: https://registry.hub.docker.com/u/dockerfile/mongodb/
+ You may assume that you have already authenticated to the AWS APIs using an appropriate method. There is no need to include this in your solution.

# Context 
The aim of this exercise is to test knowledge of AWS, Linux scripting and general system administration with emphasis on DevOps ways of working. There is no need to provide any access keys in your solution.

# Goal
Focus on building a solution which is robust and portable so that the script can be executed on any machine against any environment. If you solution is able to work across machines, no further work is needed. Be creative!
