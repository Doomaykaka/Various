import csv
import numpy as np
from numpy.random import choice
from numpy import genfromtxt
import datetime as dt
import os
import pandas as ps
import matplotlib.pyplot as plt
import matplotlib.ticker as mticker
import matplotlib.dates as mdates

def graph():

    MyDataFrame = ps.read_csv('data.csv')
    numbers = MyDataFrame["number"]
    num_min = numbers.min()
    num_max = numbers.max()
    num_ave = numbers.mean()
    num_disp = numbers.var()
    num_std = numbers.std()
    num_median = numbers.median()
    num_mode = mode(numbers)
    salary = MyDataFrame["salary"]
    sal_min = salary.min()
    sal_max = salary.max()
    sal_ave = salary.mean()
    sal_disp = salary.var()
    sal_std = salary.std()
    sal_median = salary.median()
    sal_mode = mode(salary)
    projects = MyDataFrame["completed_projects"]
    pjt_min = projects.min()
    pjt_max = projects.max()
    pjt_ave = projects.mean()
    pjt_disp = projects.var()
    pjt_std = projects.std()
    pjt_median = projects.median()
    pjt_mode = mode(projects)

    plt.figure(figsize=(14, 10), dpi=80)
    plt.hlines(y=projects, xmin=0, xmax=salary, color='C0', alpha=0.4, linewidth=5)
    plt.gca().set(ylabel='Number of projects', xlabel='Salary')
    plt.title('Dependence of the number of projects on salary', fontdict={'size': 20})
    plt.show()

    data=[MyDataFrame["gender"].value_counts()["Муж"],MyDataFrame["gender"].value_counts()["Жен"]]
    plt.pie(data, labels=["Men","Women"])
    plt.title("Distribution of men and women within the firm")
    plt.ylabel("")
    plt.show()

    data=MyDataFrame
    myLocator = mticker.MultipleLocator(4)
    plt.figure(figsize=(16, 10), dpi=80)
    plt.plot_date(data["start_date"],data["salary"])
    plt.gca().xaxis.set_major_locator(mdates.AutoDateLocator())
    plt.ylabel('Salary')
    plt.xlabel('Dates')
    plt.title("Salary changes over time")
    plt.show()

def mode(values):

    dict={}
    for elem in values:
        if elem in dict:
            dict[elem]+=1
        else:
            dict[elem]=1
    v = list(dict.values())
    k = list(dict.keys())

    return k[v.index(max(v))]

def generate():

    MyData = [["number", "full_name", "gender", "birth_date", "start_date", "division", "position", "salary",
               "completed_projects"]]
    Gender = ["Муж", "Жен"]
    Surnames = ["Антипов", "Бабаев", "Вавилов", "Галкин", "Данилин", "Евсюткин", "Жеглов", "Задорнов", "Ивачев",
                "Кабаков", "Лабутин",
                "Маврин", "Назаров", "Овсеев", "Павлов", "Райкин", "Савочкин", "Табаков", "Уваров", "Фандеев",
                "Хабалов", "Царёв", "Чадов",
                "Шаляпин", "Щукин", "Эвентов", "Юров", "Ягодин"]
    Initials = ["А", "Б", "В", "Г", "Д", "Ж", "З", "И", "К", "Л", "М", "Н", "Р", "С", "Т", "У", "Ф", "Э", "Ю", "Я"]
    Divisions = ["Отдел информационной безопасности", "Отдел разработки ПО", "Отдел контроля качества и процесов",
                 "Отдел развития ИТ", "Отдел поддержки ИТ"]
    Positions = [["Руководитель отдела информационной безопасности", "Специалист", "Начинающий специалист"],
                 ["Руководитель отдела разработки ПО", "Senior разработчик", "Middle разработчик"],
                 ["Руководитель отдела контроля качества и процесов", "Специалист", "Работник"],
                 ["Руководитель отдела развития ИТ", "Специалист", "Работник"],
                 ["Руководитель отдела поддержки ИТ", "Специалист", "Работник"]]

    for i in range(1, 2000):
        np.random.seed(i)
        num = i
        full_name = ""
        gend = ""
        birth = ""
        start = ""
        div = ""
        pos = ""
        salary = 0
        completed_projects = 0

        gender = np.random.randint(0, 2)
        gend = Gender[gender]
        if (gender != 0):
            full_name = Surnames[np.random.randint(0, 27)] + "а " + Initials[np.random.randint(0, 19)] + "." + Initials[
                np.random.randint(0, 19)] + "."
        else:
            full_name = Surnames[np.random.randint(0, 27)] + " " + Initials[np.random.randint(0, 19)] + "." + Initials[
                np.random.randint(0, 19)] + "."
        current_date = dt.date.today()
        year = current_date.year - np.random.randint(0, 11)
        month = np.random.randint(1, 13)
        day = np.random.randint(1, 29)
        start = str(day) + "." + str(month) + "." + str(year)
        byear = year - np.random.randint(18, 31)
        bmonth = np.random.randint(1, 13)
        bday = np.random.randint(1, 29)
        birth = str(bday) + "." + str(bmonth) + "." + str(byear)
        divis = np.random.randint(0, 5)
        div = Divisions[divis]
        posit = choice([0, 1, 2], 1, [0.1, 0.3, 0.6])[0]
        pos = Positions[divis][posit]
        salary = np.random.randint(10000, 20001) * (5 - divis) * (3 - posit)
        completed_projects = np.random.randint(1, 21) * (3 - posit)

        MyData.append([num, full_name, gend, birth, start, div, pos, salary, completed_projects])

    for per in MyData:
        for param in per:
            print(param, end=" , ")
        print("")

    with open("data.csv", mode="w", encoding='utf-8') as w_file:
        file_writer = csv.writer(w_file, delimiter=",", lineterminator="\r")
        for per in MyData:
            file_writer.writerow(per)

def np_stat():

    MyData = []

    with open('data.csv', mode="r", encoding='utf-8') as f:
        reader = csv.reader(f)
        for row in reader:
            data=row
            MyData.append([data[0],data[1],data[2],data[3],data[4],data[5],data[6],data[7],data[8]])

    for per in MyData:
        for param in per:
            print(param, end=" , ")
        print("")

    MyData=np.array(MyData)
    numbers=np.array(MyData[:,0])
    numbers=np.delete(numbers, 0)
    numbers=[int(item) for item in numbers]
    num_min=np.min(numbers)
    num_max=np.max(numbers)
    num_ave= np.average(numbers)
    num_disp=np.var(numbers)
    num_std=np.std(numbers)
    num_median=np.median(numbers)
    num_mode=mode(numbers)
    salary=np.array(MyData[:,7])
    salary=np.delete(salary,0)
    salary = [int(item) for item in salary]
    sal_min=np.min(salary)
    sal_max=np.max(salary)
    sal_ave=np.average(salary)
    sal_disp=np.var(salary)
    sal_std=np.std(salary)
    sal_median = np.median(salary)
    sal_mode=mode(salary)
    projects=np.array(MyData[:,8])
    projects = np.delete(projects, 0)
    projects = [int(item) for item in projects]
    pjt_min = np.min(projects)
    pjt_max = np.max(projects)
    pjt_ave = np.average(projects)
    pjt_disp = np.var(projects)
    pjt_std = np.std(projects)
    pjt_median = np.median(projects)
    pjt_mode=mode(projects)
    print(numbers)

    print("")
    print("Statistical characteristics")
    print("")
    print("For column number: min="+str(num_min)+" ; max="+str(num_max)+" ; ave="+str(num_ave)+" ; disp="+str(num_disp)+" ; std="+str(num_std)+" ; median="+str(num_median)+" ; mode="+str(num_mode))
    print("For column salary: min=" + str(sal_min) + " ; max=" + str(sal_max) + " ; ave=" + str(sal_ave) + " ; disp=" + str(sal_disp) + " ; std=" + str(sal_std) + " ; median=" + str(sal_median) + " ; mode=" + str(sal_mode))
    print("For column projects: min=" + str(pjt_min) + " ; max=" + str(pjt_max) + " ; ave=" + str(pjt_ave) + " ; disp=" + str(pjt_disp) + " ; std=" + str(pjt_std) + " ; median=" + str(pjt_median) + " ; mode=" + str(pjt_mode))

def ps_stat():

    MyDataFrame = ps.read_csv('data.csv')
    numbers=MyDataFrame["number"]
    num_min = numbers.min()
    num_max = numbers.max()
    num_ave = numbers.mean()
    num_disp = numbers.var()
    num_std = numbers.std()
    num_median = numbers.median()
    num_mode = mode(numbers)
    salary = MyDataFrame["salary"]
    sal_min = salary.min()
    sal_max = salary.max()
    sal_ave = salary.mean()
    sal_disp = salary.var()
    sal_std = salary.std()
    sal_median = salary.median()
    sal_mode = mode(salary)
    projects = MyDataFrame["completed_projects"]
    pjt_min = projects.min()
    pjt_max = projects.max()
    pjt_ave = projects.mean()
    pjt_disp = projects.var()
    pjt_std = projects.std()
    pjt_median = projects.median()
    pjt_mode = mode(projects)
    print(MyDataFrame.to_string())

    print("")
    print("Statistical characteristics")
    print("")
    print("For column number: min=" + str(num_min) + " ; max=" + str(num_max) + " ; ave=" + str(num_ave) + " ; disp=" + str(num_disp) + " ; std=" + str(num_std) + " ; median=" + str(num_median) + " ; mode=" + str(num_mode))
    print("For column salary: min=" + str(sal_min) + " ; max=" + str(sal_max) + " ; ave=" + str(sal_ave) + " ; disp=" + str(sal_disp) + " ; std=" + str(sal_std) + " ; median=" + str(sal_median) + " ; mode=" + str(sal_mode))
    print("For column projects: min=" + str(pjt_min) + " ; max=" + str(pjt_max) + " ; ave=" + str(pjt_ave) + " ; disp=" + str(pjt_disp) + " ; std=" + str(pjt_std) + " ; median=" + str(pjt_median) + " ; mode=" + str(pjt_mode))

if __name__ == '__main__':

    print("Generation process")
    print("")

    generate()

    print("")
    print("CSV file created (Press Enter)")
    input()

    print("Numpy statistics")
    print("")

    np_stat()

    print("")
    print("CSV file analyzed (Press Enter)")
    input()

    print("Pandas statistics")
    print("")

    ps_stat()

    print("")
    print("CSV file analyzed (Press Enter)")
    input()

    print("Graphics")
    print("")

    graph()

    print("")
    print("CSV file removed")
    os.remove("data.csv")

