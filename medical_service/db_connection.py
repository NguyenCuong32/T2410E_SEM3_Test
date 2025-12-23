import pyodbc

def get_connection():
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};"
        "SERVER=LAPTOP-3H7QNPMQ\\SQLEXPRESS;"
        "DATABASE=medical_service;"
        "Trusted_Connection=yes;"
    )
    return conn
