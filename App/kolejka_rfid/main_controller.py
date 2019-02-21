from view import *
from rfid import *
from queue_requests import *
import json


def event(refer, popup):
    r = RFID()
    r.green_light_diode()
    id = r.get_rfid()
    r.green_off_diode()
    req = AzureData()
    status, response = req.addStudent(refer, id)
    if (status == 200):
        return response
    else:
        return "Error server offline"


def get_queue_status():
    r = AzureData()
    data = r.getAllqueues()
    data = json.loads(data)
    return data


class Program:
    def __init__(self):
        self.root = MainWindow()


def main():
    program = Program()


if __name__ == '__main__':
    main()

