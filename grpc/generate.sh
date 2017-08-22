#!/bin/bash
# usage: ./generate.sh PROTOFILE.proto
# todo: generate for each file of each folder automatically

GRPC_VERSION="1.4.1"
PACKAGE_FOLDER=~/.nuget/packages/grpc.tools/${GRPC_VERSION}/tools/linux_x64 
PROTOC=${PACKAGE_FOLDER}/protoc
GRPC_CSHARP_PLUGIN=${PACKAGE_FOLDER}/grpc_csharp_plugin
PROTO_FOLDER=../../prodrink.proto

chmod +x $PROTOC
chmod +x $GRPC_CSHARP_PLUGIN
$PROTOC \
    -I${PROTO_FOLDER} \
    --csharp_out . --grpc_out . ${PROTO_FOLDER}/catalog/$1 \
    --plugin=protoc-gen-grpc=$GRPC_CSHARP_PLUGIN

