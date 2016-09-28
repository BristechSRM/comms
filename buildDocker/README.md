#Building the comms service 
To build, copy the contents of this folder to an empty directory on a linux VM. The VM must have docker installed.  
If you are already on linux, no need to do anything. 

## Create a quick, clean vm for Building

1. If necessary, install docker-machine
https://docs.docker.com/machine/install-machine/
2. Create a docker macine called builder 
Note: The memory specified is required to build the frontend. If you plan to build all services + the frontend, use this value or higher (Thanks node.js). 
If you are only building services, you can use the default memory setting. 
`docker-machine create builder --driver virtualbox --virtualbox-memory 1500`

3. Move to this directory (BuildDocker) and copy all files to the vm
`cd {pathToThisDirectory}`

`docker-machine scp -r . builder:~/comms-buildDocker`

## Performing build

Then simply run `./dockerBuild.sh` to build the docker image for comms from the latest master.

If you want to test the build before something is in master, 
you'll need to change dockerBuild to clone a branch instead of master. The command for this is
`git clone --depth 1 -b my-branch https://github.com/BristechSRM/comms.git $DIR/source`

## Pushing to docker
Make sure the image has been created correctly with a recent created time
`docker images comms:latest`

Now simply perform `docker login` with the credentials for bristechsrm and then run 
`docker push bristechsrm/comms`

Further details can be found here: 
https://docs.docker.com/engine/getstarted/step_six/ 