using System;
public class Solution
{
    //Task 1
    public static string solution(string S){
        int n = S.Length;
        for(int i = 0 ; i <  n - 1; i++){
            if(S[i] > S[i + 1]) return S.Substring(0 , i) + S.Substring(i + 1);
        }
        return  S.Substring(0 ,  n - 1);
    }
    
    //Task 2
    public static long solution(long[][]  A){
        int n = A.Length;
        int m = A[0].Length;
        long res = 0;
        bool [] h = new bool[n];
        bool [] v = new bool[m];
        
        for(int i = 0 ; i < n ; i++){
            for(int j = 0; j < m ; j++){
                h[i] = v[j] = true;
                long val = A[i][j];
                for(int k = 0 ; k < n ; k++){
                    if(h[k] == true) continue;
                    for(int l = 0 ; l < m ; l++){
                        if(v[l] == true) continue;
                        res = Math.Max(res , val + A[k][l]);   
                    }
                }
            }
        }
        
        return res;
    }
    
    //Task 3
    public static int solution(int[] A){
        int n = A.Length;
        Array.Sort(A);
        int res = 0;
        for(int i = 1 ; i <= n ; i++){
            res += Math.Abs(A[i - 1] - i);
        }
        return res;
        
    }
    
    public static void Main(string[] args)
    {
        
    }
}