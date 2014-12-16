#!/bin/bash

# Install the latest Ubuntu package for docker
apt-get update
apt-get install -y docker.io

# Run mongodb container
docker run -d -p 27017:27017 --name mongodb dockerfile/mongodb

