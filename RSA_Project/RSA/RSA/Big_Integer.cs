using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Big_Integer
    {
        public Big_Integer() { }
        public string add(string number1, string number2)
        {
            StringBuilder result = new StringBuilder();
            //string result = "";
            int carry = 0;
            // if num1 is longer than num2 swap them
            // num2 should be the longest string
            if (number1.Length > number2.Length)
            {
                string temp = number1;
                number1 = number2;
                number2 = temp;
            }
            int num1_len = number1.Length;
            int num2_len = number2.Length;
            int diff_len = num2_len - num1_len;
            //step1 add numbers from mun1 and num2 till num1 length which is the shortest one
            for (int i = num1_len - 1; i >= 0; i--)
            {
                int sum = ((int)(number1[i] - '0') + (int)(number2[i + diff_len] - '0') + carry);
                result.Append((char)(sum % 10 + '0'));
                carry = sum / 10;
            }
            //step2 add the rest numbers of num2 which is the longest one
            for (int i = diff_len - 1; i >= 0; i--)
            {
                int sum = ((int)(number2[i] - '0') + carry);
                result.Append((char)(sum % 10 + '0'));
                carry = sum / 10;
            }
            //step3 add carry
            if (carry > 0)
            {
                result.Append((char)(carry + '0'));
            }
            //reverse result string
            string result_builder = result.ToString();
            char[] newresult = result_builder.ToCharArray();
            Array.Reverse(newresult);
            StringBuilder final_result = new StringBuilder();
            for (int i = 0; i < newresult.Length; i++)
            {
                if (newresult[i] == '0')
                {
                    newresult[i] = '$';
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < newresult.Length; i++)
            {
                if (newresult[i] != '$')
                {
                    final_result.Append(newresult[i]);
                }
            }
            return final_result.ToString();
            //return new string(newresult);
            //return result;
        }
        public string sub(string number1, string number2)
        {
            StringBuilder result = new StringBuilder();
            // x and y holds the values of number1 and number2 as integers  
            int x = 0;
            Int32.TryParse(number1, out x);
            int y = 0;
            Int32.TryParse(number2, out y);
            //if both strings are equal in value
            if (number1 == number2)
            {
                return ("0");
            }
            //if both strings are different then check lentgh and value
            else
            {
                //if num2 is longer than num1 then swap
                // num1 should be the longest one
                if (number2.Length > number1.Length)
                {
                    string temp = number2;
                    number2 = number1;
                    number1 = temp;
                }
                //if number2 is larger than number1 then swap them
                else if (number1.Length == number2.Length)
                {
                    if (y > x)
                    {
                        int temp = y;
                        y = x;
                        x = temp;
                    }
                    //number1 = x.ToString();
                    //number2 = y.ToString();
                }
                int num1_len = number1.Length;
                int num2_len = number2.Length;
                int diff_len = num1_len - num2_len;
                int borrow = 0;
                //step1 subtract num1 from num2 until the length of num2 which is the shortest
                for (int i = num2_len - 1; i >= 0; i--)
                {
                    int diff = (((int)number1[i + diff_len] - (int)'0') - ((int)number2[i] - (int)'0') - borrow);
                    if (diff < 0)
                    {
                        diff += 10;
                        borrow = 1;
                    }
                    else
                    {
                        borrow = 0;
                    }
                    result.Append(diff.ToString());
                }
                //step2 accumalate rest bits from num1 which is the longest one to the result
                for (int i = diff_len - 1; i >= 0; i--)
                {
                    if (number1[i] == '0' && borrow > 0)
                    {
                        result.Append("9");
                        continue;
                    }
                    int diff = (((int)number1[i] - (int)'0') - borrow);
                    if (i > 0 || diff > 0)
                    {
                        result.Append(diff.ToString());
                    }
                    borrow = 0;
                }
                string result_builder = result.ToString();
                char[] newresult = result_builder.ToCharArray();
                Array.Reverse(newresult);
                StringBuilder final_result = new StringBuilder();
                for (int i = 0; i < newresult.Length; i++)
                {
                    if (newresult[i] == '0')
                    {
                        newresult[i] = '$';
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 0; i < newresult.Length; i++)
                {
                    if (newresult[i] != '$')
                    {
                        final_result.Append(newresult[i]);
                    }
                }
                return final_result.ToString();
                //return new string(newresult);
                //return result;
            }
        }
        public int equallystrings(ref string number1, ref string number2)
        {
            int num1_length = number1.Length;  //O(1)
            int num2_length = number2.Length;   //O(1)
            //both strings must be equal in length
            if (num1_length > num2_length)  //O(1)
            {
                number2 = number2.PadLeft(num1_length, '0');  //O(1) //shift the string from left and put 0's according to the biggest lenght
                return number2.Length;  //O(1)
            }
            else if (num1_length < num2_length)  //O(1)
                number1 = number1.PadLeft(num2_length, '0');  //O(1)
            return number1.Length;  //O(1) //return the number1Lenght when length1 >= length2

        }
        public string mul(string number1, string number2)
        {
            //function that return the length after equalling them
            int num_length = equallystrings(ref number1, ref number2); //O(1)
            int mid = num_length / 2;  //O(1)
            //base case
            if (num_length == 1) //O(1)
            {
                return (((number1[0]) - '0') * ((number2[0]) - '0')).ToString();  //O(1)
            }
            else if (num_length == 0) //O(1)
            {
                return "0";
            }
            //divide step
            string a = number1.Substring(0, mid);  //O(N)
            string b = number1.Substring(mid);  //O(N)
            string c = number2.Substring(0, mid);  //O(N)
            string d = number2.Substring(mid);  //O(N)
            //conquer step
            string ac = mul(a, c);  //recursive step //T(N/2)
            string bd = mul(b, d);  //recursive step //T(N/2)
            string z = mul(add(a, b), add(c, d));  //recursive step //T(N/2)
            string firstTerm = add(ac, bd);  //O(N)
            string remainingTerm = sub(z, firstTerm);  //O(N)
            //combine step
            string ans = add(ac.PadRight((ac.Length + (num_length - mid) * 2), '0'), remainingTerm.PadRight((remainingTerm.Length + (num_length - mid)), '0')); //O(N)
            string finalAns = add(ans, bd); //O(N)
            return finalAns;
        }


        public class div
        {
            public string qoutient = "";
            public string remainder = "";
            public div() { }
        }

        div bi = new div();
        public div mydiv(string number1, string number2)
        {
            bool num2 = false;
            if (number2.Length > number1.Length)//O(1)
            {
                bi.qoutient = "0";
                bi.remainder = number1;
                return bi;
            }
            else if (number1.Length == number2.Length)//O(1)
            {
                for (int i = 0; i < number1.Length; i++)
                {
                    if ((number2[i] - '0') > (number1[i] - '0'))
                    {
                        num2 = true;
                        break;
                    }
                    else if ((number1[i] - '0') > (number2[i] - '0'))
                        break;
                }
                if (num2)
                {
                    bi.qoutient = "0";
                    bi.remainder = number1;
                    return bi;
                }
            }
            bool check_num = false;
            string double_number2 = add(number2, number2);
            bi = mydiv(number1, double_number2);
            bi.qoutient = add(bi.qoutient, bi.qoutient);  //q=2q
            if (bi.remainder.Length < number2.Length)     //if (r < b)
            {
                return bi;                                //return (q, r)
            }
            else if (bi.remainder.Length == number2.Length)
            {
                for (int i = 0; i < number2.Length; i++)
                {
                    if (bi.remainder[i] < number2[i])      //if (r < b)
                    {
                        check_num = true;
                        break;
                    }
                    else if (bi.remainder[i] > number2[i])
                        break;
                }
            }

            if (check_num) return bi;                      //return (q, r)
            else                                           // return (q + 1, r - b)
            {
                string last_qoutient = add(bi.qoutient, "1");
                string last_remainder = sub(bi.remainder, number2);
                bi.qoutient = last_qoutient;
                bi.remainder = last_remainder;
                return bi;
            }
        }


        //function genertaes public key
        public int public_key(int e, string n,int p,int q)
        {
            int key = 0;
            string phi_val = "";
            int phi = 0;
            
            /////compute p and q




            n = mul(p.ToString(), q.ToString());
            ////compute phi=(p-1)(q-1)
            int first_term = p - 1;
            int second_term = q - 1;
            phi_val = mul(first_term.ToString(), second_term.ToString());
            Int32.TryParse(phi_val, out phi);

            ////choose e
            
                Random r = new Random();                                                                                 
               
                e = r.Next(1, phi);
                if (e%phi !=1)
                {
                return e;
                    
                }
            


            return key;
        }


        // Utility function to do modular  
        // exponentiation. It returns (x^y) % p 
        public int power(int x, int y, int p)
        {

            int res = 1; // Initialize result 

            // Update x if it is more than  
            // or equal to p 
            x = x % p;

            while (y > 0)
            {

                // If y is odd, multiply x with result 
                if ((y & 1) == 1)
                    res = (res * x) % p;

                // y must be even now 
                y = y >> 1; // y = y/2 
                x = (x * x) % p;
            }

            return res;
        }

        // This function is called for all k trials.  
        // It returns false if n is composite and  
        // returns false if n is probably prime. 
        // d is an odd number such that d*2<sup>r</sup> 
        // = n-1 for some r >= 1 
        public bool millerTest(int d, int n)
        {

            // Pick a random number in [2..n-2] 
            // Corner cases make sure that n > 4 
            Random r = new Random();
            int a = 2 + (int)(r.Next() % (n - 4));

            // Compute a^d % n 
            int x = power(a, d, n);

            if (x == 1 || x == n - 1)
                return true;
            // Keep squaring x while one of the 
            // following doesn't happen 
            // (i) d does not reach n-1 
            // (ii) (x^2) % n is not 1 
            // (iii) (x^2) % n is not n-1 
            while (d != n - 1)
            {
                x = (x * x) % n;
                d *= 2;

                if (x == 1)
                    return false;
                if (x == n - 1)
                    return true;
            }

            // Return composite 
            return false;
        }

        // It returns false if n is composite  
        // and returns true if n is probably  
        // prime. k is an input parameter that  
        // determines accuracy level. Higher  
        // value of k indicates more accuracy. 
        public bool isPrime(int n, int k)
        {

            // Corner cases 
            if (n <= 1 || n == 4)
                return false;
            if (n <= 3)
                return true;

            // Find r such that n = 2^d * r + 1  
            // for some r >= 1 
            int d = n - 1;

            while (d % 2 == 0)
                d /= 2;

            // Iterate given nber of 'k' times 
            for (int i = 0; i < k; i++)
                if (millerTest(d, n) == false)
                    return false;

            return true;
        }

    }
}

 





