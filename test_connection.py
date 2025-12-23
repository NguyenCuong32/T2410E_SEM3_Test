from database import create_connection

conn = create_connection()

if conn:
    print("✅ KẾT NỐI SQL SERVER THÀNH CÔNG")
    conn.close()
else:
    print("❌ KẾT NỐI THẤT BẠI")
