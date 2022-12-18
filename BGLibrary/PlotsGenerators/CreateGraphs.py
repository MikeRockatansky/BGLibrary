import pandas as pd



def Execute(query, xlabel, ylabel, cursor):
    cursor.execute(query)
    result = pd.DataFrame(cursor.fetchall(), columns=[xlabel, ylabel])
    return result


def CreateQuery(xlabel, ylabel, left_lim=0, right_lim=0):
    if left_lim == 0 and right_lim == 0:
        query = f'''
        SELECT {xlabel}, {ylabel} FROM BoardGames
        '''
    else:
        query = f'''    
        SELECT {xlabel}, {ylabel} FROM BoardGames
        WHERE {xlabel} <= {right_lim} AND {xlabel} >= {left_lim}
        '''
    return query
