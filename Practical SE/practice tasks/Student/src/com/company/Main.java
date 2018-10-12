package com.company;

import java.util.ArrayList;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Student s = new Student("John Co", "Greek", 3.5f);
        s.printStudentInfo();
        Scanner sc = new Scanner(System.in);
        ArrayList<Student> list = new ArrayList<>();
        list.add(s);

        for (int i = 0; i < 2; i++) {
            System.out.println("Print info about the student (name, nationality, average)");
            String name_ = sc.nextLine();
            String nationality_ = sc.nextLine();
            String avg = sc.nextLine();
            float average_ = Float.parseFloat(avg);
            Student newStudent = new Student(name_, nationality_, average_);
            newStudent.printStudentInfo();
            list.add(newStudent);
        }
        //list.get(0).getName();

        for (int i = 0; i < list.size(); i++) {
            System.out.println(list.get(i));
        }

        Student best = list.get(0);
        Student worst = best;

        for (int i = 0; i < list.size(); i++) {
            if (list.get(i).getAverage() < worst.getAverage()) {
                worst = list.get(i);
            }
            if (list.get(i).getAverage() > best.getAverage()) {
                best = list.get(i);
            }
        }

        System.out.println("Best student: " + best.getName() + "   Worst student:" + worst.getName());

        int countAverages = 0;
        float averageOfTheAverages = s.averageOfAll(list);
        System.out.println(averageOfTheAverages);

        for (int i = 0; i < list.size(); i++) {
            if (list.get(i).getAverage() > averageOfTheAverages) {
                countAverages++;
            }
        }
        System.out.println(countAverages);
    }
        /*for (Student ss: list){
            System.out.println(ss);
        }*/
}
