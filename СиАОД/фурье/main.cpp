#include <vector>
#include <cmath>
#include "matplotlibcpp.h"

using namespace std;
namespace plt = matplotlibcpp;

class furie{
private:
    int n;
    vector<double> x_v; vector<double> x_vn;
    vector<double> y_v; vector<double> y_vn;
    double a {0}; //начало отрезка для симпсона | концом будет период
    int N;
    double T;
    pair<double, double> x;
    function<double(double, int)> func1 {[](double xi, int k){return f(xi) * cos(k * xi);}};    //левая часть члена ряда фурье
    function<double(double, int)> func2 {[](double xi, int k){return f(xi) * sin(k * xi);}};    //правая часть члена ряда фурье
    function<double(double, int)> func3 {[](double xi, int k){return f(xi) * 1;}};
public:
    //Математическая функция для разложения ряда фурье
    static double f(double xi){
        return 5 * xi;
    }

    //Функция для вычисления интеграла
    static double simpson(double a, double b, int n, int k, function<double(double, int)>& func){
        double h = (b - a) / n;
        double sum = func(a, k) + func(b, k);
        for (int i = 1; i < n; i++) {
            if (i % 2 == 0) {
                sum += 2 * func(a + i * h, k);
            }
            else {
                sum += 4 * func(a + i * h, k);
            }
        }
        return (h / 3) * sum;
    }

    //Функция для аппроксимации ряда фурье
    void furie_approx(){
        for (double i {x.first}; i < x.second; i += 0.05){
            x_v.push_back(i);
        }

        //Вычисление первого коэффициента(не члена ряда)
        int o = 1; //костыль №1
        double a0 = 2 / T * simpson(a, T, n, o, func3);

        double sum = 0;
        double func_furie;
        for(double xk : x_v) {
            for (int k{1}; k < N; k += 1) {
                double ak = 2 / T * simpson(0, T, n, k, func1);
                double bk = 2 / T * simpson(a, T, n, k, func2);
                sum += (ak * cos(k * xk) + bk * sin(k * xk));
            }
            func_furie = (a0 / 2 + sum);
            y_v.push_back(func_furie);
            sum = 0;
        }
    }

    //Функция для вычисления начальной функции для ряда фурье
    void furie_f(){
        x_vn = x_v;
        for(double i : x_vn){
            y_vn.push_back(f(i));
        }
    }

    //Функция для вывода ряда фурье
    void furie_paint(){
        furie_approx();
        furie_f();
        plt::figure_size(1000, 780);
        plt::plot(x_v, y_v, "r");
        plt::plot(x_vn, y_vn, "b");
        plt::xlim(0, 100);
        plt::title("ряд Фурье");
        plt::show();
    }

    // конструктор класса
    furie(double T, int N, pair<double, double> x, int n){
        this -> x = x;
        this -> T = T;
        this -> N = N;
        this -> n = n;
    }
};

int main(){
    pair<double, double> x {-50 , 50}; //отрезок области опредений
    int N = 15; //Количество членов ряда Фурье
    double T = 2 * M_PI; //Период функции
    int n = 100; //Число разбиений для симпсона

    furie funk_furie {T, N, x, n};
    funk_furie.furie_paint();
}