#!/bin/bash

# Navigate to folder
# chmod +x udp.sh 
# ./udp.sh 
# echo -n "$1" | nc -4u -w1 google.com 80
# echo "nginx.status_200:1|c" >google.com 80

def_host=192.168.10.1
def_port=8889

HOST=${2:-$def_host}
PORT=${3:-$def_port}

echo -n "$1" | nc -4u -w1 $HOST $PORT

#./udp.sh "command" 
#./udp.sh "takeoff" 
#./udp.sh "land" 