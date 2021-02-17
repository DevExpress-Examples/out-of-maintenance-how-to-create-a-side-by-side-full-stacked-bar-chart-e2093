namespace SideBySideFullStackedBarChart {
    public struct GenderAgeInfo {
        readonly string gender;
        readonly string age;

        public string Gender { get { return gender; } }
        public string Age { get { return age; } }

        public GenderAgeInfo(string gender, string age) {
            this.gender = gender;
            this.age = age;
        }
        public override string ToString() {
            return Gender + ": " + Age;
        }
    }
}
