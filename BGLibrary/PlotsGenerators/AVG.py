import sqlite3, sys
import CreateGraphs as CG
import matplotlib.pyplot as plt


def avg_time_based(xlabel="YearPublished", ylabel="Average", left_lim=1960, right_lim=2020, kind="line"):
    query = CG.CreateQuery(xlabel, ylabel, left_lim, right_lim)
    data = CG.Execute(query, xlabel, ylabel, cursor).dropna()

    data = data.sort_values(xlabel)

    data = data.pivot(columns=xlabel, values=ylabel)
    data = data[data > 0]
    print(data)
    data = data.mean()
    fig = data.plot(xlabel=xlabel, ylabel=f"AVG {ylabel}", x=data.index, y=data, kind=kind).get_figure()
    fig.savefig(f"wwwroot/plot.png")
    plt.clf()


sqliteConnection = sqlite3.connect('boardgames.db')
cursor = sqliteConnection.cursor()

avg_time_based()

y = sys.argv[1]
lLim = int(sys.argv[2])
rLim = int(sys.argv[3])

avg_time_based(ylabel=y, left_lim=lLim, right_lim=rLim)

sqliteConnection.close()
