#include <iostream>
#include <cmath>
#include <vector>
#include <functional>
#include <numbers>

//Функция интерполяции по кубическому сплейну
double interpolate(const std::vector<double>& xp, const std::vector<double>& fp, double x) {
    auto it = lower_bound(xp.begin(), xp.end(), x);
    if (it == xp.begin()) return fp[0];
    if (it == xp.end()) return fp.back();
    int pos = it - xp.begin();
    double x1 = xp[pos - 1], x2 = xp[pos];
    double y1 = fp[pos - 1], y2 = fp[pos];
    return y1 + (x - x1) * (y2 - y1) / (x2 - x1);
}

//Расчетная функция
void splain(std::vector<double>& x, std::vector<double>& y, std::vector<double>& x0, std::vector<std::function<double(double)>>& func){
    //Задаём значения точкам по x и y
    for (double i = 1; i <= 1.2001; i += 0.04) {
        x.push_back(i);
        y.push_back(func[2](i));
    }
    for (double xi : x0) {
        double t = interpolate(x, y, xi);
        std::cout << "Интерполяция точки " << xi << ": " << t << std::endl;
    }
}


int main(){
    std::vector<double> x;  //Значения точек по x координате
    std::vector<double> y;  //Значения точек по y координате
    std::vector<double> x0; //Значение точек, которые надо вычислить

    //Заданные начальные значения
    x0 = {1.05, 1.09, 1.13, 1.15, 1.17};

    //функции
    auto f1 = [](double x){return pow(std::numbers::e, x);};
    auto f2 = [](double x){return sinh(x);};
    auto f3 = [](double x){return cosh(x);};
    auto f4 = [](double x){return sin(x);};
    auto f5 = [](double x){return cos(x);};
    auto f6 = [](double x){return log(x);};
    auto f7 = [](double x){return pow(std::numbers::e, -x);};
    std::vector<std::function<double(double)>> func {f1, f2, f3, f4, f5, f6, f7}; //Вектор функций

    splain(x, y, x0, func);
}
