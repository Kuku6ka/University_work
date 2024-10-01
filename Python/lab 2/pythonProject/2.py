from math import sqrt, erf, inf
from scipy import integrate, stats
from numpy import mean
import numpy as np

def approx_poly(x, t, r):
    M = [[] for _ in range(r + 1)]
    b = []
    for l in range(r + 1):
        for q in range(r + 1):
            M[l].append(sum(list(map(lambda z: z**(l + q), t))))
        b.append(sum(xi * ti ** l for xi, ti in zip(x, t)))
    a = Gaus(M, b)
    return a[::-1]

def Gaus(matrix, b):
    n = len(matrix)

    # Forward elimination
    for i in range(n):
        # Find the maximum element in this column
        max_row = i
        for k in range(i + 1, n):
            if abs(matrix[k][i]) > abs(matrix[max_row][i]):
                max_row = k

        # Swap rows if necessary
        if max_row != i:
            matrix[i], matrix[max_row] = matrix[max_row], matrix[i]
            b[i], b[max_row] = b[max_row], b[i]

        # Make pivot element equal to 1
        pivot = matrix[i][i]
        matrix[i] = [element / pivot for element in matrix[i]]
        b[i] /= pivot

        # Subtract this row from other rows
        for k in range(n):
            if k != i:
                factor = matrix[k][i]
                matrix[k] = [matrix[k][j] - factor * matrix[i][j] for j in range(n)]
                b[k] -= factor * b[i]

    # Back substitution
    x = [0] * n
    for i in range(n - 1, -1, -1):
        x[i] = b[i]
        for k in range(i + 1, n):
            x[i] -= matrix[i][k] * x[k]
        x[i] /= matrix[i][i]

    return x

def f_norm(x, mu=0, s=1):
    return (1 + erf((x - mu) / sqrt(2) / s)) / 2

def p_value(max_delta_x, sigma):
    return 2 * integrate.quad(f_norm, -inf, -max_delta_x, args=(0, sigma))[0]

def polynomial_value(coefficients, x):
    result = 0
    power = len(coefficients) - 1
    for coefficient in coefficients:
        result += coefficient * (x ** power)
        power -= 1
    return result

def Sigma(data):
    squared_data = [x ** 2 for x in data]
    Mean = mean(data)
    mean_of_squares = mean(squared_data)
    square_of_mean = Mean ** 2
    return sqrt(mean_of_squares - square_of_mean)


def calculate_power(p_value, alpha=0.05):
    power = 1 - alpha if p_value <= alpha else np.exp(-stats.norm.cdf(np.sqrt(2 * np.log(1 / alpha)) - stats.norm.ppf(p_value)))
    return power


def main():
    x = [12, 16, 24, 36, 23, 12, 65, 75, 33]
    t = [i for i in range(len(x))]

    h_0 = 3
    h_a = 28

    k_0 = approx_poly(x, t, h_0)
    k_a = approx_poly(x, t, h_a)

    x_0 = [polynomial_value(k_0, ti) for ti in t]
    x_a = [polynomial_value(k_a, ti) for ti in t]

    max_del_x_0 = 0
    max_del_x_a = 0

    for i in t:
        del_x_0 = abs(polynomial_value(k_0,i) - x[i])
        del_x_a = abs(polynomial_value(k_a,i) - x[i])
        max_del_x_0 = max(max_del_x_0, del_x_0)
        max_del_x_a = max(max_del_x_a, del_x_a)

    sigma_0 = Sigma(x_0)
    sigma_a = Sigma(x_a)

    p_0_value = p_value(max_del_x_0, sigma_0)
    p_a_value = p_value(max_del_x_a, sigma_a)

    print(f"p_value_0 = {p_0_value} , p_value_a = {p_a_value}")

    power_0 = calculate_power(p_value(max_del_x_0, sigma_0))
    power_a = calculate_power(p_value(max_del_x_a, sigma_a))

    print(f"power_0 = {power_0} , power_a = {power_a}")
main()