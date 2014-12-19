#!/bin/bash

# Install the latest Ubuntu package for docker
sudo apt-get update
sudo apt-get install -y docker.io

mongoDbPort=27017

# Run the MongoDB container
sudo docker run -d -p $mongoDbPort:$mongoDbPort --name mongodb dockerfile/mongodb

# Open MongoDB port on firewall
sudo ufw allow $mongoDbPort
