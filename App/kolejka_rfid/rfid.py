import RPi.GPIO as GPIO
import SimpleMFRC522



class RFID:
    def __init__(self):
        RFID.init_pin()
        self.card_reader = SimpleMFRC522.SimpleMFRC522()

    @staticmethod
    def init_pin():
        GPIO.setmode(GPIO.BCM)
        GPIO.setwarnings(False)
        GPIO.setup(17, GPIO.OUT)    #red
        GPIO.output(17, GPIO.LOW)
        GPIO.setup(27, GPIO.OUT)    #green
        GPIO.output(27, GPIO.LOW)
        GPIO.setup(15, GPIO.OUT)    #buzzer
        GPIO.output(15, GPIO.LOW)

    def get_rfid(self):
        try:
            card_id, text = self.card_reader.read()
            print (card_id)
            return card_id

        except Exception as ex:
            print("Error: ", ex)

    def green_light_diode(self):
        GPIO.output(17, GPIO.HIGH)

    def green_off_diode(self):
        GPIO.output(17, GPIO.LOW)

