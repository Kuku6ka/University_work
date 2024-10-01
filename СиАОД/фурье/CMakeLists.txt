cmake_minimum_required(VERSION 3.28)
project(furie2)

include_directories(freeglut/include/GL)
include_directories(matplotlib-cpp)

find_package(Python COMPONENTS Development NumPy)
include_directories(${Python_INCLUDE_DIRS})

set(CMAKE_CXX_STANDARD 26)
add_executable(furie2 main.cpp matplotlib-cpp/matplotlibcpp.h)
target_link_libraries(furie2 ${Python_LIBRARIES})