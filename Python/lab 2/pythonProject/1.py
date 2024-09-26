from math import sqrt, erf, pi, exp
from scipy import integrate

# нормальное распределение
def f_norm(x, mu=0, s=1):
    return (1 + erf((x - mu) / sqrt(2) / s)) / 2

# нормальное распределение плотности
def rho_norm(x, mu=0,s=1):
    return 1 / sqrt(2 * pi * s) * exp(-(x - mu) ** 2 / 2 / s ** 2)

# вероятность ошибки 1-го рода
def p_value(x, mu=0, s=1):
    return 2*(1-f_norm(x,mu,s)) if x >= mu else 2*f_norm(x,mu,s)

# Обратная функция распределения
def inv_f_norm(p, mu, s, t=0.001):
    if mu != 0 or s != 1:
        return mu + s * inv_f_norm(p, 0, 1, t)
    low_x, low_p = -100, 0
    hi_x, hi_p = 100, 1
    while hi_x - low_x > t:
        mid_x = (low_x + hi_x) / 2
        mid_p = f_norm(mid_x)
        if mid_p < p:
            low_x, low_p = mid_x, mid_p
        elif mid_p > p:
            hi_x, hi_p = mid_x, mid_p
        else:
            break
    return mid_x

'''def proverca(P_value, alpha, power, beta, n):
    if P_value > alpha and power > beta:
        print(f"Требуется часов {n}")
        print(f"при этом мощность = {power}")
'''

def main():
    p_0 = 3  # первая гипотеза
    p_a = 4  # вторая гипотеза
    x = 500 / 8 # колличество плиток
    alpha = 0.05
    beta = 0.8

    p_0 = p_0 / 8
    p_a = p_a / 8

    for n in range(100, 1000):
        mu_0 = n * p_0
        mu_a = n * p_a

        sigma_0 = sqrt(n * p_0 * (1 - p_0))
        sigma_a = sqrt(n * p_a * (1 - p_a))

        s_low = inv_f_norm(alpha / 2, mu_0, sigma_0)
        s_high = 2 * mu_0 - s_low

        P_value = p_value(x, mu_0, sigma_0)

        power_hiposis = 1 - (integrate.quad(rho_norm, s_low, s_high, args=(mu_a, sigma_a)))[0]

        if P_value > alpha and power_hiposis > beta:
            print(f"Требуется часов {n}")
            print(f"при этом мощность = {power_hiposis}")
            break


main()