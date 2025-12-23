from db_connect import get_connection

def add_doctors():
    conn = get_connection()
    cursor = conn.cursor()

    for i in range(5):
        print(f"\nEnter doctor {i + 1}")
        full_name = input("Doctor name: ")
        specialization = input("Specialization: ")

        sql = """
        INSERT INTO doctors (full_name, specialization)
        VALUES (%s, %s)
        """
        cursor.execute(sql, (full_name, specialization))

    conn.commit()
    conn.close()
    print("✔ Doctors added successfully")
