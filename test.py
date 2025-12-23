from db import get_connection

conn = get_connection()

if conn:
    print("Kết nối SQL Server thành công!")
    conn.close()
else:
    print("Kết nối thất bại!")
