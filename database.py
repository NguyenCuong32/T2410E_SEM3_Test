# database.py
import mysql.connector
from mysql.connector import Error

def create_connection():
    """
    Câu 1: Connection to database
    Hàm tạo kết nối đến CSDL MySQL
    """
    connection = None
    try:
        connection = mysql.connector.connect(
            host='localhost',
            port=8889,     
            database='medical_service',
            user='root',            
            password=''     
        )
        if connection.is_connected():
            return connection
    except Error as e:
        print(f"Lỗi kết nối database: {e}")
    return connection