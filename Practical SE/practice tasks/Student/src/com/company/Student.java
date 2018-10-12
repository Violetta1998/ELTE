package com.company;

import java.util.ArrayList;

public class Student {
    private String name;
    private final String nationality;
    private float average;
    private int nameChangeCounter = 0;

    Student(String name, String nationality, float average) {
        this.name = name;
        this.nationality = nationality;
        this.average = average;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        if (nameChangeCounter < 3) {
            this.name = name;
            nameChangeCounter++;
        }
    }

    public String getNationality() {
        return nationality;
    } //we don't have Setter for nationality, as it cannot be changed

    public float getAverage() {
        return average;
    }

    public void setAverage(float average) {
        this.average = average;
    }

    public void printStudentInfo() {
        System.out.println(this.name + ", nationality: " + this.nationality + ", " + this.average);
    }

    public float averageOfAll(ArrayList<Student> list) {
        float average = 0;
        for (int i = 0; i < list.size(); i++) {
            average += list.get(i).getAverage();
        }
        average = average / list.size();
        return average;
    }

    @Override
    public String toString() {
        return "Name:" + name + ", Nationality: " + nationality + ", Average: " + average;
    }
}

