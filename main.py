from patient import add_patients
from doctor import add_doctors
from appointment import add_appointments
from report import report_appointments, report_today_appointments

def show_menu():
    print("\n===== MEDICAL SERVICE SYSTEM =====")
    print("1. Thêm 3 bệnh nhân")
    print("2. Thêm 5 bác sĩ")
    print("3. Thêm 3 lịch hẹn")
    print("4. Report tất cả lịch hẹn")
    print("5. Report lịch hẹn hôm nay")
    print("0. Thoát")

while True:
    show_menu()
    choice = input("Chọn chức năng: ")

    if choice == "1":
        add_patients()

    elif choice == "2":
        add_doctors()

    elif choice == "3":
        add_appointments()

    elif choice == "4":
        report_appointments()

    elif choice == "5":
        report_today_appointments()

    elif choice == "0":
        print("Thoát chương trình")
        break

    else:
        print("Lựa chọn không hợp lệ, vui lòng chọn lại!")
