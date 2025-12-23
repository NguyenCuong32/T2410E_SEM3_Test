from db_connection import get_connection
from patient_service import insert_patients
from doctor_service import insert_doctors
from appointment_service import insert_appointments
from report_service import appointment_report, today_appointments

def main():
    db = get_connection()
    cursor = db.cursor()

    insert_patients(cursor)
    insert_doctors(cursor)
    insert_appointments(cursor)

    db.commit()

    appointment_report(cursor)
    today_appointments(cursor)

    cursor.close()
    db.close()

if __name__ == "__main__":
    main()
