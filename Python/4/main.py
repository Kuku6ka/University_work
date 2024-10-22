from time import time
from dsmltf import gradient_descent, minimize_stochastic
from math import pi, cos, sin
import matplotlib.pyplot as plt

x = list()

#радианы для 500 ряда
base = [2*pi*(i/500) for i in range(500)]

#функция частичной суммы ряда фурьё
def furie(k,a):
    global base
    return a[0]+a[1]*cos(base[k]) + a[2]*sin(base[k])+a[3]*cos(2*base[k])+a[4]*sin(2*base[k])

#функция для градиентного спуска
def F(a:list) -> float:
    global x
    return sum([(x[j]-furie(j,a))**2 for j in range(500)])

#функция для стохастического спуска
def f(i,a):
    global x
    return (x[i]-furie(i,a))**2

def main():
    #Коэффициенты
    k = 1
    L = k/100
    omega = 1000/k
    dt = 2*pi/1000

    #генерирование 500 значений ряда
    global x
    x = [0, (-1) ** k * dt]
    for i in range(2, 500):
        x.append(x[i-1]*(2+dt*L*(1-x[i-2]**2))- x[i-2]*(1+dt**2+dt*L*(1-x[i-2]**2))+dt**2*sin(omega*dt))

    #вычисление градиентного спуска
    time_g_0 = time()

    params_list = [0.5, -0.2, 0.9, 0, 1.1]

    a0 = gradient_descent(F,params_list, 100, 0.4)

    time_g_1 = time()

    print(f"время выполнения градиентного спуска(сек): {time_g_1-time_g_0}")
    print(a0[0], a0[1])
    #вычисление стохастического спуска
    time_s_0 = time()

    a1 = minimize_stochastic(f,[i for i in range(500)],[0]*500,[0]*5, 0.09, 900)

    time_s_1 = time()

    print(f"время выполнения стохастического спуска(сек): {time_s_1-time_s_0}")
    print(a1[0], a1[1])

    plt.plot(base, x, label='Изначальная функция', marker='o')
    plt.plot(base, [furie(i, a0[0]) for i in range(500)], label=f'Градиентный спуск', linestyle='-')
    plt.plot(base, [furie(i, a1[0]) for i in range(500)], label=f'Стохатичный градиентный спуск', linestyle='-')
    plt.xlabel('x')
    plt.ylabel('y')
    plt.legend()
    plt.grid(True)
    plt.show()



if __name__ == '__main__':
    main()