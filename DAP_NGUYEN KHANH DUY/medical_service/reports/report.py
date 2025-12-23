def show_report(cursor):
    cursor.execute(
        """
        SELECT p.patient_id, p.full_name, p.date_of_birth, p.gender, p.address,
               d.full_name, a.reason, a.appointment_date
        FROM appointments a
        JOIN patients p ON a.patient_id = p.patient_id
        JOIN doctors d ON a.doctor_id = d.doctor_id
        """
    )
    for row in cursor.fetchall():
        print(row)
def show_today_appointments(cursor):
    cursor.execute(
        """
        SELECT p.address, p.patient_id, p.full_name, p.date_of_birth, p.gender,
               d.full_name, a.status
        FROM appointments a
        JOIN patients p ON a.patient_id = p.patient_id
        JOIN doctors d ON a.doctor_id = d.doctor_id
        WHERE DATE(a.appointment_date) = CURDATE()
        """
    )
    for row in cursor.fetchall():
        print(row)
