import pandas
import matplotlib.pyplot as pyplot
import pylab
from sklearn.preprocessing import StandardScaler
from sklearn.tree import DecisionTreeClassifier
from sklearn.svm import SVC
from sklearn.neural_network import MLPClassifier

#Читаем данные из csv
df = pandas.read_csv('Cyber_salaries.csv')
teach_df = df.iloc [:675]
test_df = df.iloc [675:]

#Подготавливаем данные
def preparation(df):
    df = df.copy()
    df = df.drop('salary',axis=1)
    df = df.drop('salary_currency', axis=1)
    df['employment_type'] = pandas.factorize(df['employment_type'])[0]
    df['job_title'] = pandas.factorize(df['job_title'])[0]
    df['employee_residence'] = pandas.factorize(df['employee_residence'])[0]
    df['company_location'] = pandas.factorize(df['company_location'])[0]
    df['company_size'] = pandas.factorize(df['company_size'])[0]
    X = df.drop('experience_level', axis=1)
    y = df['experience_level']
    X = pandas.DataFrame(X, index=X.index, columns=X.columns)
    return X, y

#Масштабируем данные
scaler = StandardScaler()
X_teach, y_teach = preparation(teach_df)
X_teach = scaler.fit_transform(X_teach)
X_test, y_test = preparation(test_df)
X_test = scaler.fit_transform(X_test)

#Прогоняем тестовые данные через классификаторы и выводим точность предсказаний

#Перцептрон
mlp = MLPClassifier(random_state = 1, max_iter = 300).fit(X_teach, y_teach)
mlp_predictions = pandas.Series(mlp.predict(X_test))
print('Точность предсказаний многослойного перцептрона: ' + str(mlp.score(X_test, y_test)*100) + '%')

#Дерева решений
dtc = DecisionTreeClassifier(random_state = 0).fit(X_teach, y_teach)
dtc_predictions = pandas.Series(dtc.predict(X_test))
print('Точность предсказаний дерева решений: ' + str(dtc.score(X_test, y_test)*100) + '%')

#Метод опорных векторов
svm = SVC(kernel = 'rbf').fit(X_teach, y_teach)
svm_predictions = pandas.Series(svm.predict(X_test))
print('Точность предсказаний методом опорных векторов: ' + str(svm.score(X_test, y_test)*100) + '%')

#Отрисовываем графики с предсказанными и реальными значениями уровней опыта сотрудников

#Перцептрон
pylab.figure(figsize=(20,10))
pylab.subplot(1, 2, 1)
pyplot.pie(y_test.value_counts().sort_index(), labels = sorted(y_test.unique()), autopct='%1.1f%%')
pyplot.title('Реальные уровни опыта сотрудников')
pylab.subplot(1, 2, 2)
pyplot.pie(mlp_predictions.value_counts().sort_index(), labels = sorted(mlp_predictions.unique()), autopct='%1.1f%%')
pyplot.title('Уровни опыта сотрудников предсказанные многослойным перцептроном')
pyplot.show()

#Дерева решений
pylab.figure(figsize=(20,10))
pylab.subplot(1, 2, 1)
pyplot.pie(y_test.value_counts().sort_index(), labels = sorted(y_test.unique()), autopct='%1.1f%%')
pyplot.title('Реальные уровни опыта сотрудников')
pylab.subplot(1, 2, 2)
pyplot.pie(dtc_predictions.value_counts().sort_index(), labels = sorted(dtc_predictions.unique()), autopct='%1.1f%%')
pyplot.title('Уровни опыта сотрудников предсказанные деревом решений')
pyplot.show()

#Метод опорных векторов
pylab.figure(figsize=(20,10))
pylab.subplot(1, 2, 1)
pyplot.pie(y_test.value_counts().sort_index(), labels = sorted(y_test.unique()), autopct='%1.1f%%')
pyplot.title('Реальные уровни опыта сотрудников')
pylab.subplot(1, 2, 2)
pyplot.pie(svm_predictions.value_counts().sort_index(), labels = sorted(svm_predictions.unique()), autopct='%1.1f%%')
pyplot.title('Уровни опыта сотрудников предсказанные методом опорных векторов')
pyplot.show()