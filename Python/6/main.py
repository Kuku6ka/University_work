from dsmltf import knn_classify, principal_components
import csv
from genser import transform_to


def csv_read():
    data = list()
    with open('iris.csv') as csvfile:
        reader = csv.reader(csvfile, delimiter=',')
        for row in reader:
            data.append(row)
        data.pop(0)
    return data

def sort_data(data:list):
    result_data = list()
    for row in data:
        values = [float(x) for x in row[:-1]]
        variety = row[-1]
        result_data.append((values, variety))
    return result_data

def main():
    src_data = csv_read()
    data = sort_data(src_data)

    # k ближайших (голый)
    for k in range(1, 15):
        n_correct = 0
        for flower in data:
            values, variety = flower
            other_flowers = [other_flower for other_flower in data if other_flower != flower]
            predicted_br = knn_classify(k, other_flowers, values)
            if predicted_br == variety:
                n_correct += 1
        print(k, "соседей:", n_correct, "правильных из", len(data))

    # k ближайших с уменьшением размерности по PCA
    print("П.3")
    x = [i[0] for i in data]
    y = [i[1] for i in data]
    data1 = principal_components(x, 2)
    PCA_data = list()
    for j in range(len(data1)):
        PCA_data.append((data1[j], y[j]))

    for k in range(1, 15):
        n_correct = 0
        for flower in PCA_data:
            values, variety = flower
            other_flowers = [other_flower for other_flower in PCA_data if other_flower != flower]
            predicted_br = knn_classify(k, other_flowers, values)
            if predicted_br == variety:
                n_correct += 1
        print(k, "соседей:", n_correct, "правильных из", len(PCA_data))

    #
    data2, _ = transform_to(x, 2)
    SER_data = list()
    for j in range(len(data2)):
        SER_data.append((data2[j], y[j]))

    print("П.4")
    for k in range(1, 15):
        n_correct = 0
        for flower in SER_data:
            values, variety = flower
            other_flowers = [other_flower for other_flower in SER_data if other_flower != flower]
            predicted_br = knn_classify(k, other_flowers, values)
            if predicted_br == variety:
                n_correct += 1
        print(k, "соседей:", n_correct, "правильных из", len(PCA_data))

if __name__ == '__main__':
    main()