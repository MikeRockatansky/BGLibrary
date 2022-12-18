import CreateGraphs as CG
import sqlite3, sys
import matplotlib.pyplot as plt


def time_based(xlabel="YearPublished", ylabel="Name", left_lim=0, right_lim=0, kind="line", value=None, isRise=False):
    query = CG.CreateQuery(xlabel, ylabel, left_lim, right_lim)
    data = CG.Execute(query, xlabel, ylabel, cursor).dropna()

    data = data.sort_values(xlabel)
    if value is not None:
        data = data[data[ylabel] == value]

    data = data.pivot(columns=xlabel, values=ylabel).count()
    if isRise:
        for i in range(len(data) - 1):
            data[data.index[i + 1]] += data[data.index[i]]

    fig = data.plot(xlabel=xlabel, ylabel=f"Number of Games Where {ylabel} - {value}", x=data.index, y=data, kind=kind).get_figure()
    fig.savefig(f"wwwroot/plot.png")
    plt.clf()


sqliteConnection = sqlite3.connect('boardgames.db')
cursor = sqliteConnection.cursor()

y = sys.argv[1]
value = int(sys.argv[2])
lLim = int(sys.argv[3])
rLim = int(sys.argv[4])
time_based(ylabel=y, value=value, left_lim=lLim, right_lim=rLim)

sqliteConnection.close()
