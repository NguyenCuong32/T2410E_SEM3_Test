from db.connection import get_connection
from services.patient_service import add_patients
from services.doctor_service import add_doctors
from services.appointment_service import add_appointments
from reports.report import show_report, show_today_appointments

def main():
    connection = get_connection()
    cursor = connection.cursor()
    add_patients(cursor)
    add_doctors(cursor)
    add_appointments(cursor)
    connection.commit()
    show_report(cursor)
    show_today_appointments(cursor)

    cursor.close()
    connection.close()
if __name__ == "__main__":
    main()
