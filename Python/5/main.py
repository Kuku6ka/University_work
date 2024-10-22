import ast
from functools import partial
from dsmltf import dot, scale, train_test_split, gradient_descent, negate, log_likelyhood, sigmoid
from random import random

def extract_data():
    data = list()
    with open('data.txt', 'r') as file:
        for line in file:
            data.append(ast.literal_eval(line.strip()))
    return data

def main():
    data = extract_data()

    x = [row[:-1] for row in data]
    y = [row[-1] for row in data]

    # Шкалируем данные
    sc_x = scale(x)

    # Разбиваем данные на обучающую и тестовые выборки

    x_train, x_test, y_train, y_test = train_test_split(sc_x, y, 0.33)
    print(sum(y_test))

    fn = partial(log_likelyhood, x_train, y_train)

    beta_0 = [random() for _ in range(4)]

    beta_hat = gradient_descent(negate(fn), beta_0)[0]

    true_positives, false_negatives, false_positives, true_negatives = 0, 0, 0, 0
    for x_i, y_i in zip(x_test, y_test):
        predict = sigmoid(dot(beta_hat, x_i))
        if y_i == 1 and predict >= 0.5:
            true_positives += 1
        elif y_i == 1:
            false_negatives += 1
        elif predict >= 0.5:
            false_positives += 1
        else:
            true_negatives += 1

    precision = true_positives / (true_positives + false_positives)
    recall = true_positives / (true_positives + false_negatives)
    print(precision, recall)





if __name__ == '__main__':
    main()