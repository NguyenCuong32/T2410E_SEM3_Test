from db import get_connection

def add_patients():
    # Kết nối database
    conn = get_connection()
    if conn is None:
        return

    cursor = conn.cursor()

    print("=== NHẬP THÔNG TIN 3 BỆNH NHÂN ===")

    # Nhập đúng 3 bệnh nhân
    for i in range(3):
        print(f"\nBệnh nhân thứ {i + 1}")

        full_name = input("Họ tên: ")
        date_of_birth = input("Ngày sinh (YYYY-MM-DD): ")
        gender = input("Giới tính: ")
        address = input("Địa chỉ: ")
        phone = input("Số điện thoại: ")
        email = input("Email: ")

        sql = """
        INSERT INTO patients
        (full_name, date_of_birth, gender, address, phone_number, email)
        VALUES (?, ?, ?, ?, ?, ?)
        """

        cursor.execute(
            sql,
            full_name,
            date_of_birth,
            gender,
            address,
            phone,
            email
        )

    # Lưu dữ liệu
    conn.commit()

    cursor.close()
    conn.close()

    print("\nĐã thêm 3 bệnh nhân thành công!")
