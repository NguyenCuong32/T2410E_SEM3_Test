import pyodbc

def get_connection():
    try:
       
        connection_string = (
            'DRIVER={ODBC Driver 17 for SQL Server};'
            'SERVER=HUYKKEN;'
            'DATABASE=medical_service;'
            'Trusted_Connection=yes;'
        )
        connection = pyodbc.connect(connection_string)
        return connection
    except Exception as e:
        print(f"Lỗi kết nối: {e}")
        return None