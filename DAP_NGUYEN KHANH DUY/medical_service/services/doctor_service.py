def add_doctors(cursor):
    for _ in range(5):
        name = input("Doctor name: ")
        specialization = input("Specialization: ")
        cursor.execute(
            """
            INSERT INTO doctors (full_name, specialization)
            VALUES (%s, %s)
            """,
            (name, specialization)
        )
