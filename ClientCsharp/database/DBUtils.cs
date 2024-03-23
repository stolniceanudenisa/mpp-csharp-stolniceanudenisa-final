﻿using System.Collections.Generic;
using System.Data;
using ClientCsharp.ConnectionUtils;
 

namespace ClientCsharp.database;

public static class DBUtils
{
    private static IDbConnection instance = null;

    public static IDbConnection getConnection(IDictionary<string, string> props)
    {
        if (instance == null || instance.State == System.Data.ConnectionState.Closed)
        {
            instance = getNewConnection(props);
            instance.Open();
        }

        return instance;
    }

    private static IDbConnection getNewConnection(IDictionary<string, string> props)
    {
        return ConnectionFactory.getInstance().createConnection(props);
    }
}