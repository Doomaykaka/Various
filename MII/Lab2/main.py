from random import randint as ri
import numpy as np
import matplotlib.pyplot as plt

if __name__ == '__main__':
    print("Option 7")

    try:
        print("Input matrix size")
        n = int(input())
        if (n % 2) | (n < 1):
            raise Exception("The dimension must be even")
        print("Input multiplier")
        k = int(input())

        a = np.array([[ri(-10, 10) for j in range(n)] for i in range(n)])
        print("Matrix A")
        print(a)

        e = a[0: n // 2, 0: n // 2]
        b = a[0: n // 2, n // 2:]
        d = a[n // 2:, 0: n // 2]
        c = a[n // 2:, n // 2:]

        print("Submatrix e")
        print(e)
        print("Submatrix b")
        print(b)
        print("Submatrix d")
        print(d)
        print("Submatrix c")
        print(c)

        f = a.copy()

        zerocountinnonevenc = np.count_nonzero(c[:, ::2] == 0)
        print("Zero count in submatrix c for non even columns")
        print(zerocountinnonevenc)

        zerocountinevenc = np.count_nonzero(c[:, 1::2] == 0)
        print("Zero count in submatrix c for even columns")
        print(zerocountinevenc)

        if (zerocountinnonevenc > zerocountinevenc):
            b = np.flipud(a[n // 2:, n // 2:])
            c = np.flipud(a[0: n // 2, n // 2:])
        else:
            e = a[n // 2:, 0: n // 2]
            d = a[0: n // 2, 0: n // 2]

        print("New values")
        print("Submatrix e")
        print(e)
        print("Submatrix b")
        print(b)
        print("Submatrix d")
        print(d)
        print("Submatrix c")
        print(c)

        f = np.concatenate((np.concatenate((e, d), axis=0), np.concatenate((b, c), axis=0)), axis=1);

        print("Matrix F")
        print(f)

        print("Determinator of A")
        det = np.linalg.det(a)
        print(det)

        print("Diagonal sum of F")
        dia = np.trace(f)
        print(dia)

        if det > dia:
            at = np.transpose(a)
            ft = np.transpose(f)
            result = np.subtract(np.multiply(a, at), np.multiply(k, ft))
        else:
            at = np.transpose(a)
            g = np.tril(a)
            finv = np.linalg.inv(f)
            result = np.multiply(np.subtract(np.add(at, g), finv), k)

        print("Result")
        print(result)

        graph1=plt.figure()
        esize=len(e)*len(e[0])
        x = np.linspace(0, esize, esize)
        y = np.asarray(e).reshape(-1)
        plt.plot(x,y)
        plt.title("Matrix E")
        plt.show()


        graph2 = plt.figure()
        bsize = len(b) * len(b[0])
        x = np.linspace(0, bsize, bsize)
        y = np.asarray(b).reshape(-1)
        plt.plot(x, y)
        plt.title("Matrix B")
        plt.show()

        graph3 = plt.figure()
        dsize = len(d) * len(d[0])
        x = np.linspace(0, dsize, dsize)
        y = np.asarray(d).reshape(-1)
        plt.plot(x, y)
        plt.title("Matrix D")
        plt.show()

        graph4 = plt.figure()
        csize = len(c) * len(c[0])
        x = np.linspace(0, csize, csize)
        y = np.asarray(c).reshape(-1)
        plt.plot(x, y)
        plt.title("Matrix C")
        plt.show()

    except ValueError:
        print("Your value is not a number")
    except Exception as exc:
        print(exc)
        print("Restart the program")


