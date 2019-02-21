import requests
import time
import json


class AzureData:

    def __init__(self):
        r = requests.get("https://utpkolejka.azurewebsites.net/api/queueinfo")
        print(r.text)


    def addStudent(self, id, rfid):
        r = requests.post("https://utpkolejka.azurewebsites.net/api/student/" + str(id), json= {'RFID': rfid})
        print(r.text)
        print(r.status_code)
        return r.status_code, r.text

    def deleteStudent(self, id, index):
        r = requests.delete("https://utpkolejka.azurewebsites.net/api/student/" + str(id), json={'IndexNumber': index})
        print(r.text)

    def getStudentData(self, index):
        r = requests.get("https://utpkolejka.azurewebsites.net/api/student/" + str(index))
        print(r.text)


    def getQueueInfo(self, id):
        r = requests.get("https://utpkolejka.azurewebsites.net/api/queue/" + str(id))
        print(r.text)
        print(r.status_code)

    def getAllqueues(self):
        r = requests.get("https://utpkolejka.azurewebsites.net/api/queueinfo")
        print(r.text)
        return r.text
