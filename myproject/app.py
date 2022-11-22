from flask import Flask
import sys
import random

app = Flask(__name__)

@app.route("/")
def principal():
    puntos = []
    rango = 20 #Rango de aparicion de los autos
    # loop para generar carritos internos
    for i in range(10):
        puntos.append({"id": i, "x": random.uniform(-rango, rango), "y": 0.1, "z": random.uniform(-rango, rango)})
        
        # COMO HACER PRINT
        #print('así se imprime' , file=sys.stdout)

        # COMO SACAR VALORES RANDOM
        #print(random.uniform(0, 10))

    return {"carros": puntos}