import numpy as np
import scipy as sp
from numpy import mean, inf
from math import sqrt, pi, exp, erf
from scipy import integrate

def f_norm(x, mu=0, s=1):
    return (1 + erf((x - mu) / sqrt(2) / s)) / 2


def approx_poly(x, t, r):
    M = [[] for _ in range(r + 1)]
    b = []
    for l in range(r + 1):
        for q in range(r + 1):
            M[l].append(sum(list(map(lambda z: z ** (l + q), t))))
        b.append(sum(xi * ti ** l for xi, ti in zip(x, t)))

    a = Gauss(M, b)
    return a[::-1]

def Gauss(matrix, b):
    n = len(matrix)
    for i in range(n):
        max_row = i
        for k in range(i + 1, n):
            if abs(matrix[k][i]) > abs(matrix[max_row][i]):
                max_row = k
        if max_row != i:
            matrix[i], matrix[max_row] = matrix[max_row], matrix[i]
            b[i], b[max_row] = b[max_row], b[i]
        pivot = matrix[i][i]
        matrix[i] = [element / pivot for element in matrix[i]]
        b[i] /= pivot
        for k in range(n):
            if k != i:
                factor = matrix[k][i]
                matrix[k] = [matrix[k][j] - factor * matrix[i][j] for j in range(n)]
                b[k] -= factor * b[i]
    x = [0] * n
    for i in range(n - 1, -1, -1):
        x[i] = b[i]
        for k in range(i + 1, n):
            x[i] -= matrix[i][k] * x[k]
        x[i] /= matrix[i][i]

    return x

def Sigma(data):
    squared_data = [x ** 2 for x in data]
    Mean = mean(data)
    mean_of_squares = mean(squared_data)
    square_of_mean = Mean ** 2
    return sqrt(mean_of_squares - square_of_mean)

def p_value(max_delta_x, sigma):
    return 2 * integrate.quad(f_norm, -inf, -max_delta_x, args=(0, sigma))[0]

def polynomial_value(coefficients, x):
    result = 0
    power = len(coefficients) - 1
    for coefficient in coefficients:
        result += coefficient * (x ** power)
        power -= 1
    return result

#x = [10,20,30,40]
x = [70,62,38,114,37,139,192,14,199,38,198,52,45,67,24,23,76,56] # Данные аукционов
t = [i for i in range(len(x))]
h0 = 15
hA = 10

k0 = approx_poly(x,t,h0)
kA = approx_poly(x,t,hA)


# Вычисляем аппроксимированные значения
x0 = [polynomial_value(k0, ti) for ti in t]
xA = [polynomial_value(kA, ti) for ti in t]

max_del_x_0 = 0
max_del_x_a = 0

for i in t:
    del_x0 = abs(polynomial_value(k0,i) - x[i])
    del_xA = abs(polynomial_value(kA,i) - x[i])
    max_del_x_0 = max(max_del_x_0, del_x0)
    max_del_x_a = max(max_del_x_a, del_xA)

sigma0 = Sigma(x0)
sigmaA = Sigma(xA)

p0_value = p_value(max_del_x0, sigma0)
pA_value = p_value(max_del_xA, sigmaA)
print(f"P-value для полинома {h0} степени:\n {p0_value}\nP-value для полинома {hA} степени:\n {pA_value}")