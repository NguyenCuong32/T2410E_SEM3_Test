import pyodbc

def get_connection():
    try:
        connection = pyodbc.connect(
            "DRIVER={ODBC Driver 17 for SQL Server};"
            "SERVER=localhost\\SQLEXPRESS;"
            "DATABASE=medical_service;"
            "UID=sa;"
            "PWD=123;"
            "TrustServerCertificate=yes;"
        )
        return connection
    except Exception as e:
        print("Lỗi kết nối database:")
        print(e)
        return None
