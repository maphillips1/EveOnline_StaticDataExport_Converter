import yaml
import json
import pathlib
import os
from pathlib import Path
import time

def convertFilesInDirectory(inDir, outDir):
    if os.path.isdir(inDir):
        print('processing ' + inDir + ' as directory')
        dirList = os.listdir(inDir)
        for item in dirList:
            fullName = inDir + '/' + item
            if os.path.isdir(fullName):
                extendedOutDirName = outDir + '/' + item
                convertFilesInDirectory(fullName, extendedOutDirName)
            else:

                print('Converting ' + fullName + ' from yaml to json.')
                with open(fullName, 'r', encoding='utf8') as input_yaml_file:
                    inYaml = yaml.safe_load(input_yaml_file)
                    if inYaml != None:
                        Path(outDir).mkdir(parents=True, exist_ok=True)
                        fullOutputName = outDir + '/' + item.replace('yaml', 'json')
                        with open(fullOutputName, 'w+', encoding='utf8') as outputJson:
                            json.dump(inYaml, outputJson)
                            outputJson.close()
                    input_yaml_file.close()



def convertSDEToJSON():
    startingDirectory = 'sde'
    yaml_FSD = startingDirectory + '/fsd'
    yaml_BSD =  startingDirectory + '/bsd'
    yaml_Uni =  startingDirectory + '/universe'

    endingDirectory = 'json_sde'
    json_FSD = endingDirectory + '/fsd'
    json_BSD = endingDirectory + '/bsd'
    json_Uni = endingDirectory + '/universe'


    print('processing FSD')
    startTime = time.time()
    convertFilesInDirectory(yaml_FSD, json_FSD)
    finishedFSD = time.time()
    print('processing BSD')
    convertFilesInDirectory(yaml_BSD, json_BSD)
    finishedBSD = time.time()
    print('processing Universe')
    convertFilesInDirectory(yaml_Uni, json_Uni)
    finishedUniverse = time.time()

    print(startTime)
    print(finishedFSD)
    print(finishedBSD)
    print(finishedUniverse)

convertSDEToJSON()