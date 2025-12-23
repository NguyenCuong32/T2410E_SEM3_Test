import pyodbc

def create_connection():
    """
    Câu 1: Connection to database
    Hàm tạo kết nối đến CSDL SQL Server
    """
    try:
        connection = pyodbc.connect(
            "DRIVER={ODBC Driver 17 for SQL Server};"
            "SERVER=PHAMCHIDUC\\SQLEXPRESS;"
            "DATABASE=medical_service;"
            "UID=sa;"
            "PWD=phamduc18;"
        )
        return connection
    except Exception as e:
        print(f"Lỗi kết nối database: {e}")
        return None
