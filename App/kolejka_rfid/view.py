from Tkinter import *
import tkFont
from main_controller import event
from main_controller import get_queue_status
import threading
import time


class MainWindow():

   def __init__(self):

        self.root = Tk()

        self.root.geometry("1024x600")
        self.root.resizable(0, 0)
        self.root.config(cursor="none")
        self.root.overrideredirect(1)
        self.root.bind('<Escape>', self.close)

        mainfont = tkFont.Font(family='calibri light', size=18, weight='bold')

        background_image = PhotoImage(file="resources/background.png")
        background_label = Label(self.root, image=background_image)
        background_label.place(x=0, y=0, relwidth=1, relheight=1)

        info = PhotoImage(file="resources/info.png")
        self.info_frame = Label(self.root, compound=CENTER ,text = "Informatyka\n Stosowana", font = mainfont, image=info, bd=0)
        self.info_frame.grid(row=1, column=0, padx=20, pady=10)

        self.info_frame2 = Label(self.root, compound=CENTER ,text = "Teleinformatyka",font = mainfont, image=info, bd=0)
        self.info_frame2.grid(row=1, column=1)

        self.info_frame3 = Label(self.root, compound=CENTER ,text = "Elektrotechnika",font = mainfont, image=info, bd=0)
        self.info_frame3.grid(row=1, column=2, padx=20, )

        en = PhotoImage(file="resources/flaga_anglii.png")
        self.flagEn = Button(self.root, image = en, bd = 0, pady=10)
        self.flagEn.place(x = 808, y = 10)

        pl = PhotoImage(file="resources/flaga_polski3.png")
        self.flagPl = Button(self.root, image = pl,  bd=0, pady=10, state = "disabled")
        self.flagPl.place(x= 808, y = 130)


        img = PhotoImage(file="resources/button.png")
        self.b1 = Button(self.root, compound=CENTER, text="WYBIERZ", font=mainfont, command=lambda:self.ChooseEvent(self.root, 1), image=img, bd=0,
                  highlightthickness=0, width=0, borderwidth=0, padx=0, pady=0, disabledforeground="black")
        self.b1.grid(row=2, column=0)

        self.b2 = Button(self.root, compound=CENTER, text="WYBIERZ", font=mainfont, command=lambda:self.ChooseEvent(self.root, 2), image=img, bd=0,
                  highlightthickness=0, width=0, borderwidth=0, padx=0, pady=0, disabledforeground="black")
        self.b2.grid(row=2, column=1)

        self.b3 = Button(self.root, compound=CENTER, text="WYBIERZ", font=mainfont, command=lambda:self.ChooseEvent(self.root, 3), image=img, bd=0,
                  highlightthickness=0, width=0, borderwidth=0, padx=0, pady=0,  disabledforeground="black")
        self.b3.grid(row=2, column=2)

        self.red = PhotoImage(file="resources/button_red.png")
        self.yellow = PhotoImage(file="resources/button_yel.png")
        self.green = PhotoImage(file="resources/button.png")

        threading.Thread(target=self.check_status).start()
        self.root.mainloop()

   def close(self, event):
        self.root.withdraw()
        sys.exit()

   def check_status(self):

       while True:
        data = get_queue_status()
        self.b1.config(image=self.return_image_status(data[0]), text=(data[0]["status"]).upper(),
                       state="normal" if data[0]["status"] == "otwarta" else "disabled")
        self.b2.config(image=self.return_image_status(data[1]), text=(data[1]["status"]).upper(),
                       state="normal" if data[1]["status"] == "otwarta" else "disabled")
        self.b3.config(image=self.return_image_status(data[2]), text=(data[2]["status"]).upper(),
                       state="normal" if data[2]["status"] == "otwarta" else "disabled")
        self.b1.update()
        self.b2.update()
        self.b3.update()

        time.sleep(30)



   def return_image_status(self, data):
        if (data["status"] == "otwarta"):
            return self.green
        elif(data["status"] == "wstrzymana"):
            return self.yellow
        else:
            return self.red



   def ChooseEvent(self, root, refer):
        p = PopUp(root, refer)


class PopUp:

   def __init__(self, root, refer):
      self.refer = refer
      self.popup = Toplevel(root)
      self.popup.config(cursor="none")
      self.frames = [PhotoImage(file='resources/loading-gif.gif', format='gif -index %i' % (i)) for i in range(16)]
      self.popup.grab_set()
      self.popup.geometry("600x300+212+150")
      self.popup.resizable(0, 0)
      self.popup.overrideredirect(1)
      mainfont = tkFont.Font(family='calibri light', size=20, weight='bold')
      popupimg = PhotoImage(file="resources/pop_window.png")
      self.popuplabel = Label(self.popup,text = "ZBLIZ LEGITYMACJE DO\nCZYTNIKA RFID",font = mainfont,wraplength= 500, compound=CENTER, image=popupimg)
      self.popuplabel.place(x=0, y=0, relwidth=1, relheight=1)
      exit_img = PhotoImage(file = "resources/exit.png")
      bt5 = Button(self.popuplabel, image = exit_img,command = lambda: exit_window(self.popup),  bd = 0, highlightthickness=0, width=0,borderwidth=0, padx=0, pady=0)
      bt5.place(x=558, y=-2)
      self.popup.after(100, self.handler)

      self.loadingLabel = Label(self.popuplabel, bg = "#EBA576")
      self.loadingLabel.place(x=266, y = 207)
      self.popup.after(0, self.update, 0)
      self.popup.mainloop()

   def handler(self):

       def func():
           try:
               response = event(self.refer, self.popup)
               self.popup.after_cancel(self.update)
               self.loadingLabel.destroy()
               self.popuplabel.config(text=response)
               self.popup.update()
               time.sleep(4)
               exit_window(self.popup)
           except Exception as e:
               print("Error ", e)

       threading.Thread(target=func).start()

   def update(self, ind):
       frame = self.frames[ind]
       ind += 1
       self.loadingLabel.configure(image=frame)
       self.popup.after(100, self.update, 0 if ind >= 16 else ind)

def exit_window(popup):
   popup.destroy()

