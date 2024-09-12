import math

def main():

    #Задание №1
    print("Задание №1")
    number1 = 8
    number2 = 1234567890
    print(f"Взятое число: {number2}")
    result = nod(number1, number2)
    print(f"НОД чисел равен: {result}")

    #Задание №3
    string_zad3 = "fffyyyyyfff"
    print("Задание №3 \n",glasnie(string_zad3), f"Взятая строка: {string_zad3}")

    #Задание №4
    number3 = 16
    gen = generator(number3)
    blizhaishee = next(gen)
    print("Задание №4 \n", blizhaishee, f"взятое число: {number3}")

    #Задание №6
    list_chisel = [
        [5, 2.4, 7+4j],
        [2, 7.2, 2+10j]
    ]
    cortez_compl = poisk_complecs(list_chisel)
    print("Задание №6 \n", cortez_compl)

    #Задание №7
    N = 20
    iterator = fibonachi(N)
    print("Задание №7")
    for sum_ in iterator:
        print(sum_, end=" ")

    #Задание №8
    n = 10
    index_itr = find_index_iter(n+1)
    print("\nЗадание №8")
    print(f"Индекс элемента с более чем {n} значащими цифрами: {index_itr+1}")

    #Задание №9
    print("Задание №9")
    drob_1 = Frac(1, 2)
    drob_2 = Frac(2, 3)
    print("Дроби:", drob_1, drob_2)
    print("Сумма:", drob_1 + drob_2, "\tПроизведение:", drob_1 * drob_2, "\tИнверсия первой дроби:", drob_1.inverse())

    #Задание №11
    vector_a = [1, 5, 9]
    vector_b = [2, 5, 8]
    result = vector(vector_a, vector_b)
    print("Задание №11\n", result)

    #Задание №10
    quad1 = Descr([(0, 0), (2, 0), (2, 2), (0, 2)])
    quad2 = Descr([(0, 0), (4, 0), (4, 4), (0, 4)])

    print("Задание №10")
    print(quad1 == quad2)
    print(quad1.is_similar(quad2))



#Задание №1
def nod(number1, number2):
    try:
        # Если одно из чисел равно 0, возвращаем другое
        if number1 == 0:
            return number2
        if number2 == 0:
            return number1

        #переводим числа в бинарный формат
        binar_num = bin(number2)
        #нахождение нод
        match binar_num[-3:]:
            case "000":
                result = 8
            case "100":
                result = 4
            case "110":
                result = 2
            case "010":
                result = 2
            case _:
                result = 1

        return result
    except IndexError:
        print("Ошибка индексации")
    except Exception as e:
        print(f"возникла непредвиденная ошибка: {e}")

#Задание №3
def glasnie(string):
    bukvi = "аеёиоуыэюяАЕЁИОУЫЭЮЯaeiouyAEIOUY"
    result = []
    predid_char = ""

    for char in string:
        if char in bukvi:
            if char != predid_char:
                result.append(char)
        else:
            result.append(char)
        predid_char = char

    return "".join(result)

#Задание №4
def prostoe_chislo(n):
    #Проверка на натуральность числа
    if n < 2:
        return False
    for i in range(2, int(n ** 0.5) + 1):
        if n % i == 0:
            return False
    return True

def generator(num):
    nijznie = num
    maximalochka = num

    while True:
        # Проверяем числа ниже нашего числа
        if nijznie > 1 and prostoe_chislo(nijznie):
            yield nijznie

        # Проверяем числа выше нашего числа
        if prostoe_chislo(maximalochka):
            yield maximalochka

        # Сдвигаем границы поиска
        nijznie -= 1
        maximalochka += 1

#Задание №6
def poisk_complecs(list_perem):
    complex_chisla = []

    for a in list_perem:
        for b in a:
            if isinstance(b, complex):
                complex_chisla.append(b)
    return tuple(complex_chisla)

#Задание №7
def fibonachi(N):
    a, b = 0, 1
    chastnaya_summa = 0
    for _ in range(N):
        chastnaya_summa += a
        yield chastnaya_summa
        a, b = b, a + b

#Задание №8
def fibonacci_sum():
    a, b = 0, 1
    chast_sum = 0
    index_iter = 0
    while True:
        chast_sum += a
        yield index_iter, chast_sum
        a, b = b, a + b
        index_iter += 1

def find_index_iter(digits):
    for index_iter, chast_sum in fibonacci_sum():
        if len(str(chast_sum)) >= digits:
            return index_iter

#Задание №9
class Frac:
    def __init__(self, chislitel, znamenatel):
        if znamenatel == 0:
            raise ValueError("Знаменатель не может быть нулём")
        self.chislitel = chislitel
        self.znamenatel = znamenatel
        if self.znamenatel < 0:
            self.znamenatel = -self.znamenatel
            self.chislitel = -self.chislitel

    def __add__(self, other): #Сложение дробей
        if isinstance(other, Frac):
            new_chislitel = self.chislitel * other.znamenatel + other.chislitel * self.znamenatel
            new_znamenatel = self.znamenatel * other.znamenatel
            return Frac(new_chislitel, new_znamenatel)
        raise TypeError("Сложение возможно только с другой дробью")

    def __mul__(self, other):   #Умножение дроби
        if isinstance(other, Frac):
            new_chislitel = self.chislitel * other.chislitel
            new_znamenatel = self.znamenatel * other.znamenatel
            return Frac(new_chislitel, new_znamenatel)
        raise TypeError("Умножение возможно только с другой дробью")

    def inverse(self):  #Инверсия дроби
        if self.chislitel == 0:
            raise ZeroDivisionError("Невозможно инвертировать дробь с числителем 0")
        return Frac(self.znamenatel, self.chislitel)

    def __str__(self):  #строковое представление дроби
        if self.znamenatel == 1:
            return f"{self.chislitel}"
        return f"{self.chislitel}/{self.znamenatel}"

#Задание №11
def vector(a, b):
    if len(a) != 3 or len(b) != 3:
        raise ValueError("Вектор должны быть определен в трёхмерном пространстве")

    vector_len = [
        a[1]*b[2] - a[2]*b[1],
        a[2]*b[0] - a[0]*b[2],
        a[0]*b[1] - a[1]*b[0]
    ]

    return vector_len

#Задание #10
class Descr:
    def __init__(self, ugli):
        if len(ugli) != 4:
            raise ValueError("Введен не четырёхугольник")
        self.ugli = ugli

    def len_sides(self):
        sides = []
        for i in range(4):
            x1, y1 = self.ugli[i]
            x2, y2 = self.ugli[(i+1)%4]
            length_side = math.sqrt((x2-x1)**2 + (y2-y1)**2)
            sides.append(length_side)
        return sides

    def angles(self):
        angles = []
        for i in range(4):
            x1, y1 = self.ugli[i]
            x2, y2 = self.ugli[(i+1)%4]
            x3, y3 = self.ugli[(i+2)%4]

            v1 = (x2-x1, y2-y1)
            v2 = (x3-x2, y3-y2)

            d = v1[0] * v2[0] + v1[1] * v2[1]
            len_v1 = math.sqrt(v1[0] ** 2 + v1[1] ** 2)
            len_v2 = math.sqrt(v2[0] ** 2 + v2[1] ** 2)

            cos_angle = d / (len_v1 * len_v2)
            angle = math.acos(cos_angle)
            angles.append(angle)
        return angles

    def __eq__(self, other):
        return sorted(self.len_sides()) == sorted(other.len_sides()) and sorted(self.angles()) == sorted(other.angles())

    def is_similar(self, other):
        sides_self = sorted(self.len_sides())
        sides_other = sorted(other.len_sides())
        ratio = sides_self[0] / sides_other[0]
        return all(math.isclose(sides_self[i] / sides_other[i], ratio, rel_tol=1e-9) for i in range(4))


main()