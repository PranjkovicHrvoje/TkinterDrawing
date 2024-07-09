from tkinter import *
from tkinter import filedialog
import socket
import threading as thr
import sys

class LV4(Frame):

    def __init__(self):
        super().__init__()
        self.canvas = Canvas(bg="#999999", width=1000, height=800)
        self.canvas.pack()

    def draw(self, file):
            with open(file, "r") as f:
                for line in f:
                    a = line.split(" ")
                    lik = a[0]
                    boja = a[1]
                    coords = a[2:]
                    if lik == "Line":
                        l = Linija(boja, float(coords[0]), float(coords[1]), float(coords[2]), float(coords[3]))
                        l.Draw(self.canvas)
                    elif lik == "Triangle":
                        t = Trokut(boja, float(coords[0]), float(coords[1]), float(coords[2]), float(coords[3]), float(coords[4]), float(coords[5]))
                        t.Draw(self.canvas)
                    elif lik == "Rectangle":
                        p = Pravokutnik(boja, float(coords[0]), float(coords[1]), float(coords[2]), float(coords[3]))
                        p.Draw(self.canvas)
                    elif lik == "Circle":
                        k = Kruznica(boja, float(coords[0]), float(coords[1]), float(coords[2]))
                        k.Draw(self.canvas)
                    elif lik == "Ellipse":
                        e = Elipsa(boja, float(coords[0]), float(coords[1]), float(coords[2]), float(coords[3]))
                        e.Draw(self.canvas)
                    elif lik == "Polygon":
                        e = Poligon(boja, coords[0], coords[1], coords)
                        e.Draw(self.canvas)

class GrafLik:
    boja = ''
    Dx = 0
    Dy = 0

    def __init__(self, boja, x, y):
        self.Dx = x
        self.Dy = y
        self.Setboja(boja)

    def Setboja(self, boja):
        self.boja = boja

    def Getboja(self):
        return 0

    def Draw(self):
        return 0

class Linija(GrafLik):
    Lx = 0
    Ly = 0

    def __init__(self, boja, x1, y1, x2, y2):
        GrafLik.__init__(self, boja, x1, y1)
        self.Lx = x2
        self.Ly = y2

    def Draw(self, can):
        can.create_line(self.Dx, self.Dy, self.Lx, self.Ly, fill=self.boja)

class Trokut(Linija):
    Tx = 0
    Ty = 0

    def __init__(self, boja, x1, y1, x2, y2, x3, y3):
        Linija.__init__(self, boja, x1, y1, x2, y2)
        self.Tx = x3
        self.Ty = y3

    def Draw(self, can):
        can.create_line(self.Dx, self.Dy, self.Lx, self.Ly, fill=self.boja)
        can.create_line(self.Lx, self.Ly, self.Tx, self.Ty, fill=self.boja)
        can.create_line(self.Tx, self.Ty, self.Dx, self.Dy, fill=self.boja)

class Pravokutnik(GrafLik):
    Px = 0
    Py = 0

    def __init__(self, boja, x, y, height, width):
        GrafLik.__init__(self, boja, x, y)
        self.Px = x + width
        self.Py = y + height

    def Draw(self, can):
        can.create_rectangle(self.Dx, self.Dy, self.Px, self.Py, outline=self.boja, fill='')

class Poligon(GrafLik):
    p = []

    def __init__(self, boja, x, y, points):
        GrafLik.__init__(self, boja, x, y)
        if len(points) % 2 == 0:
            self.p = points
        else:
            self.p = points[:-1]

    def Draw(self, can):
        can.create_polygon(self.p, outline=self.boja, fill='')

class Kruznica(GrafLik):
    Kx = 0
    Ky = 0
    radius = 0

    def __init__(self, boja, x, y, radius):
        GrafLik.__init__(self, boja, x - radius, y - radius)
        self.Kx = x + radius
        self.Ky = y + radius

    def Draw(self, can):
        can.create_oval(self.Dx, self.Dy, self.Kx, self.Ky, outline=self.boja, fill='')

class Elipsa(Kruznica):
    def __init__(self, boja, x, y, r1, r2):
        GrafLik.__init__(self, boja, x - r1, y - r2)
        self.Kx = x + r1
        self.Ky = y + r2

    def Draw(self, can):
        can.create_oval(self.Dx, self.Dy, self.Kx, self.Ky, outline=self.boja, fill='')

def OpenFile():
    filename = filedialog.askopenfilename()
    lv4.draw(filename)

def startServer():
    def runServer():
        listensocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        port = 8000
        maxConnections = 10
        name = socket.gethostname()
        listensocket.bind(('localhost', port))
        listensocket.listen(maxConnections)
        print("Started server at " + name + " on port " + str(port))
        start_message = "Started server at " + name + " on port " + str(port)
        lv4.canvas.create_text(1000, 800, text = start_message, fill = "white", anchor = SE, font = 32)

        while True:
            try:
                (clientsocket, address) = listensocket.accept()
                print("New connection made from address: ", address)
                t = thr.Thread(target=Srv_func, args=(clientsocket,))
                t.daemon = True
                t.start()
                #print(f"Active threads: {thr.active_count() - 1}")
            except Exception as e:
                print(f"Error accepting connections: {e}", file=sys.stderr)
    
    server_thread = thr.Thread(target=runServer)
    server_thread.daemon = True
    server_thread.start()

def Srv_func(cs):
    try:
        while True:
            message = cs.recv(1024).decode()
            if not message:
                break
            print(message)
            newFile = "message.txt"
            f = open(newFile, "w")
            f.write(message)
            f.close()
            lv4.draw(newFile)
    except Exception as e:
        print(f"Error in Srv_func: {e}", file=sys.stderr)
    finally:
        cs.close()

if __name__ == '__main__':
    root = Tk()
    lv4 = LV4()
    lv4.pack(fill=BOTH, expand=YES)
    
    menubar = Menu(root)
    filemenu = Menu(menubar, tearoff=0)
    filemenu.add_command(label="Open", command=OpenFile)
    filemenu.add_command(label="Exit", command=root.destroy)
    menubar.add_cascade(label="File", menu=filemenu)

    servermenu = Menu(menubar, tearoff=0)
    servermenu.add_command(label="Start Server", command=startServer)
    menubar.add_cascade(label="Server", menu=servermenu)

    root.config(menu=menubar)
    root.mainloop()
