import sqlite3, sys
import CreateGraphs as CG
import matplotlib.pyplot as plt


def count(xlabel, ylabel="Name", left_lim=0, right_lim=0, kind="bar"):
    query = CG.CreateQuery(xlabel, ylabel, left_lim, right_lim)
    data = CG.Execute(query, xlabel, ylabel, cursor).dropna()

    data = data.sort_values(xlabel)

    data = data.pivot(columns=xlabel, values=ylabel).count()

    fig = data.plot(xlabel=xlabel, ylabel="Number of Games", kind=kind).get_figure()
    fig.savefig(f"wwwroot/plot.png")
    plt.clf()


sqliteConnection = sqlite3.connect('boardgames.db')
cursor = sqliteConnection.cursor()

x = sys.argv[1]
lLim = int(sys.argv[2])
rLim = int(sys.argv[3])

count(xlabel=x, left_lim=lLim, right_lim=rLim)

sqliteConnection.close()
