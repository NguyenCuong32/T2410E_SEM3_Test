from db_connect import get_connection

def add_patients():
    conn = get_connection()
    cursor = conn.cursor()

    for i in range(3):
        print(f"\nEnter patient {i + 1}")
        full_name = input("Full name: ")
        dob = input("Date of birth (YYYY-MM-DD): ")
        gender = input("Gender: ")
        address = input("Address: ")

        sql = """
        INSERT INTO patients (full_name, date_of_birth, gender, address)
        VALUES (%s, %s, %s, %s)
        """
        cursor.execute(sql, (full_name, dob, gender, address))

    conn.commit()
    conn.close()
    print("✔ Patients added successfully")
