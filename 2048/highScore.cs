using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;
using System.Security.Principal;
using System.Security.AccessControl;
namespace WindowsFormsApplication1
{
    public class Score{
        private string name;
        private int score;
        public void setScore(string _name, int _score){
           name=_name;
           score=_score;
        }
        
        public string getName(){ return name;}
        public int getScore(){return score;}

        public void WriteFile(FileStream file)
        {
            string s = name + " " + score.ToString()+"\r\n";
            byte[] info = new UTF8Encoding(true).GetBytes(s);
            file.Write(info, 0, info.Length);
        }

        internal void ReadFile(StreamReader file)
        {
            string line;
            int i;
            if ((line = file.ReadLine()) != null)
            {
                string tmp1, tmp2;
                for (i = line.Length-1; i > 0; --i) if (line[i]==' ') break;
                tmp1=line.Substring(0, i);
                tmp2=line.Substring(i , line.Length-i);
                setScore(tmp1, Convert.ToInt32(tmp2));
            }
            
        }
    }
    public class highScore
    {
        
        private static  Score[] leadingBoard;
        public highScore()
        {
            leadingBoard=new Score[10];
            for (int i = 0; i < 10;++i ) leadingBoard[i] = new Score();
            leadingBoard[0].setScore("Mr.333",33333);            
            leadingBoard[1].setScore("Gau Gau",14000);
            leadingBoard[2].setScore("Berserk",13000);
            leadingBoard[3].setScore("Sam",12000);
            leadingBoard[4].setScore("Rose",11000);
            leadingBoard[5].setScore("Sword",10000);
            leadingBoard[6].setScore("Guts",9000);
            leadingBoard[7].setScore("Destroyer",8000);
            leadingBoard[8].setScore("Ashes",7500);
            leadingBoard[9].setScore("Fracture",6500);
        }
        public bool putScore(int score, string name)
        {
            int i;
            for (i = 0; i < 10; ++i) if (score >= leadingBoard[i].getScore()) break;
            if (i == 10) return false;
            for (int j = 9; j > i; --j)
            {
                leadingBoard[j].setScore(leadingBoard[j-1].getName(),leadingBoard[j-1].getScore());
            }
            leadingBoard[i].setScore(name,score);
            return true;
        }
        public Score getRank(int index)
        {
            return leadingBoard[index];
        }
        public static bool HasWritePermissionOnDir(string path)
        {
            var writeAllow = false;
            var writeDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true,
                                        typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                    continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    writeDeny = true;
            }

            return writeAllow && !writeDeny;
        }
        static public void WriteFile(string p,int Mode=0){
            FileStream file;
            HasWritePermissionOnDir(p);
            if (Mode==0)
                file = new FileStream(p, FileMode.Open);
            else
                file = new FileStream(p, FileMode.Append);
            for (int i = 0; i < 10; ++i ) 
                leadingBoard[i].WriteFile(file);
            file.Close();
        }

        static public void ReadFile(string p)
        {
            
            StreamReader file = new StreamReader(p);
            for (int i = 0; i < 10; ++i)
                leadingBoard[i].ReadFile(file);
            file.Close();
        }
    }
}
