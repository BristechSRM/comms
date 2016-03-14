FROM ubuntu 

# Out of date Git, but only used for "go get"
RUN sudo apt-get update && sudo apt-get install -y git curl

RUN curl -O https://storage.googleapis.com/golang/go1.6.linux-amd64.tar.gz && \
    tar -C /usr/local -xzf go1.6.linux-amd64.tar.gz

ENV PATH /usr/local/go/bin:$PATH
# GOPATH should be set when code is mapped into this container
