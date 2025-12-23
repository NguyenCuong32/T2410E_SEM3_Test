def add_appointments(cursor):
    for _ in range(3):
        patient_id = int(input("Patient ID: "))
        doctor_id = int(input("Doctor ID: "))
        date = input("Appointment date (YYYY-MM-DD HH:MM): ")
        reason = input("Reason: ")
        cursor.execute(
            """
            INSERT INTO appointments
            (patient_id, doctor_id, appointment_date, reason)
            VALUES (%s, %s, %s, %s)
            """,
            (patient_id, doctor_id, date, reason)
        )
