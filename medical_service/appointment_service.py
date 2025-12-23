def insert_appointments(cursor):
    print("\n=== Enter 3 Appointments ===")
    for i in range(3):
        print(f"\nAppointment {i + 1}")
        patient_id = int(input("Patient ID: "))
        doctor_id = int(input("Doctor ID: "))
        date_time = input("Appointment date (YYYY-MM-DD HH:MM:SS): ")
        reason = input("Reason: ")
        status = input("Status (Pending/Done): ")

        sql = """
        INSERT INTO appointments
        (patient_id, doctor_id, appointment_date, reason, status)
        VALUES (%s, %s, %s, %s, %s)
        """
        cursor.execute(sql, (patient_id, doctor_id, date_time, reason, status))

